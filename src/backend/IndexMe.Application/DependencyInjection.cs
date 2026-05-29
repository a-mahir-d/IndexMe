using IndexMe.Application.Services;
using IndexMe.Domain.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TS.MediatR;

namespace IndexMe.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly(), typeof(Entity<>).Assembly);
        });

        services.AddScoped<TrimAndChopStringsService>();

        return services;
    }
}
