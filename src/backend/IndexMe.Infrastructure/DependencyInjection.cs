using IndexMe.Application.Abstractions;
using IndexMe.Application.Analytics;
using IndexMe.Domain.Abstractions;
using IndexMe.Domain.LinkClicks;
using IndexMe.Domain.Links;
using IndexMe.Domain.Users;
using IndexMe.Infrastructure.Analytics;
using IndexMe.Infrastructure.Context;
using IndexMe.Infrastructure.Repositories;
using IndexMe.Infrastructure.Services;
using IndexMe.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;

namespace IndexMe.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<IndexMeDbContext>((sp, options) =>
        {
            var settings = sp.GetRequiredService<IOptions<DbSettings>>().Value;
            options.UseNpgsql(settings.ConnectionString);
        });

        services.AddScoped<IUnitOfWork>(opt => opt.GetRequiredService<IndexMeDbContext>());

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILinkRepository, LinkRepository>();
        services.AddScoped<ILinkClickRepository, LinkClickRepository>();

        services.AddSingleton<IClickChannel, ClickChannel>();

        services.AddScoped<IJwtService, JwtService>();
        services.AddHttpClient<IGeoIpService, GeoIpService>().AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromMilliseconds(500)));

        services.AddHostedService<ClickBackgroundWorker>();
        return services;
    }
}
