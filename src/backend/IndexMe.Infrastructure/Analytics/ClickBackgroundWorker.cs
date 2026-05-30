using IndexMe.Application.Analytics;
using IndexMe.Domain.LinkClicks;
using IndexMe.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IndexMe.Infrastructure.Analytics;

public sealed class ClickBackgroundWorker(IClickChannel clickChannel, IServiceScopeFactory scopeFactory, ILogger<ClickBackgroundWorker> logger) : BackgroundService
{
    private const int BatchSize = 10;
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

            var affectedLinkIds = _batchBuffer.Select(c => c.LinkId).Distinct().ToList();
            string sql = @"
            WITH RankedClicks AS (
                SELECT ""Id"", 
                       ROW_NUMBER() OVER (PARTITION BY ""LinkId"" ORDER BY ""Id"" DESC) AS rn
                FROM ""LinkClicks""
                WHERE ""LinkId"" = ANY({0})
            )
            DELETE FROM ""LinkClicks""
            WHERE ""Id"" IN (SELECT ""Id"" FROM RankedClicks WHERE rn > 500);";

            await context.Database.ExecuteSqlRawAsync(sql, [affectedLinkIds], cancellationToken);

            logger.LogInformation("{Count} adet tıklama bulk edildi ve eski kayıtlar temizlendi.", _batchBuffer.Count);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "PostgreSQL bulk insert veya temizlik esnasında hata oluştu.");
        }
        finally
        {
            _batchBuffer.Clear();
        }
    }
}
