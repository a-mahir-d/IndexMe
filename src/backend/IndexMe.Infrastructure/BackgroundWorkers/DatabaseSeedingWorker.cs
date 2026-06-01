using IndexMe.Domain.Links;
using IndexMe.Domain.Users;
using IndexMe.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace IndexMe.Infrastructure.BackgroundWorkers;

public sealed class DatabaseSeedingWorker(IServiceScopeFactory scopeFactory, ILogger<DatabaseSeedingWorker> logger) : BackgroundService
{
    private readonly PeriodicTimer _timer = new(TimeSpan.FromHours(1));

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Database Seeding & Cleanup Worker başlatıldı.");
        logger.LogInformation("Uygulama başlangıç sıfırlaması tetikleniyor...");
        await CheckAndResetDatabaseAsync(stoppingToken, true);

        try
        {
            while (await _timer.WaitForNextTickAsync(stoppingToken))
            {
                await CheckAndResetDatabaseAsync(stoppingToken, false);
            }
        }
        catch (OperationCanceledException)
        {
            logger.LogWarning("Database Seeding Worker durduruluyor...");
        }
    }

    private async Task CheckAndResetDatabaseAsync(CancellationToken cancellationToken, bool force)
    {
        var currentHour = DateTime.UtcNow.Hour;

        if (force || currentHour % 4 == 0)
        {
            logger.LogInformation("Zamanı geldi (Saat UTC: {Hour}:00). Veritabanı sıfırlama işlemi başlıyor...", currentHour);

            using var scope = scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<IndexMeDbContext>();

            try
            {
                await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"LinkClicks\" RESTART IDENTITY CASCADE;", cancellationToken);
                await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"Links\" RESTART IDENTITY CASCADE;", cancellationToken);
                await context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"Users\" RESTART IDENTITY CASCADE;", cancellationToken);

                logger.LogInformation("Veritabanı tabloları temizlendi. Dummy datalar yazılıyor...");

                await SeedDummyDataAsync(context, cancellationToken);

                logger.LogInformation("Veritabanı başarıyla sıfırlandı ve dummy datalarla dolduruldu.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Veritabanı sıfırlama esnasında kritik bir hata oluştu.");
            }
        }
    }

    private static async Task SeedDummyDataAsync(IndexMeDbContext context, CancellationToken cancellationToken)
    {
        var demoUser = User.Create(username: "john_doe", email: "john_doe@indexme.com", password: "u9IFRZyZlgZFTuO7h6YCe", displayName: "John Doe", bio: "The best person in the world at being anonymous");
        var link1 = Link.Create(title: "Github", url: "https://github.com/", displayOrder: 1, user: demoUser);
        var link2 = Link.Create(title: "LinkedIn", url: "https://www.linkedin.com/", displayOrder: 2, user: demoUser);
        var link3 = Link.Create(title: "Instagram", url: "https://www.instagram.com/", displayOrder: 3, user: demoUser);

        await context.Users.AddAsync(demoUser, cancellationToken);
        await context.Links.AddRangeAsync([link1, link2, link3], cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }
}
