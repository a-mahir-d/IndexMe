using IndexMe.Application.Analytics;
using IndexMe.Domain.LinkClicks;
using IndexMe.Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IndexMe.Infrastructure.Analytics;

public sealed class ClickBackgroundWorker(IClickChannel clickChannel, IServiceScopeFactory scopeFactory, ILogger<ClickBackgroundWorker> logger) : BackgroundService
{
    private const int BatchSize = 100;
    private readonly List<LinkClick> _batchBuffer = new(BatchSize);

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Click Analytics Background Worker başlatıldı.");

        try
        {
            // Kuyruktan sürekli verileri oku (IAsyncEnumerable)
            await foreach (var click in clickChannel.ReadAllAsync(stoppingToken))
            {
                _batchBuffer.Add(click);

                // Buffer dolduysa veya kuyruk o anlık boşaldıysa veritabanına toplu yaz
                if (_batchBuffer.Count >= BatchSize)
                {
                    await FlushBatchAsync(stoppingToken);
                }
            }
        }
        catch (OperationCanceledException)
        {
            logger.LogWarning("Arka plan işçisi durduruluyor, buffer'da kalanlar kaydediliyor...");
            await FlushBatchAsync(CancellationToken.None);
        }
    }

    private async Task FlushBatchAsync(CancellationToken cancellationToken)
    {
        if (_batchBuffer.Count == 0) return;

        // DbContext Scoped olduğu için BackgroundService (Singleton) içinden factory ile çağırıyoruz.
        using var scope = scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<IndexMeDbContext>();

        try
        {
            await context.LinkClicks.AddRangeAsync(_batchBuffer, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            logger.LogInformation("{Count} adet tıklama verisi başarıyla veritabanına bulk edildi.", _batchBuffer.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Tıklama verileri kaydedilirken bir hata oluştu.");
        }
        finally
        {
            _batchBuffer.Clear();
        }
    }
}
