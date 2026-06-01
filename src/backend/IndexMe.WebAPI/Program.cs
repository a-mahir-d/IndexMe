using IndexMe.Application;
using IndexMe.Application.Abstractions;
using IndexMe.Application.Behaviors;
using IndexMe.Infrastructure;
using IndexMe.Infrastructure.Helpers;
using IndexMe.Infrastructure.Settings;
using IndexMe.WebAPI.Middlewares;
using IndexMe.WebAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using TS.MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMemoryCache();

builder.Services.AddOptions<JwtSettings>().BindConfiguration("Jwt").ValidateDataAnnotations().ValidateOnStart();
builder.Services.AddOptions<DbSettings>().BindConfiguration("Db").ValidateDataAnnotations().ValidateOnStart();
builder.Services.AddOptions<ClientSettings>().BindConfiguration("Client").ValidateDataAnnotations().ValidateOnStart();
builder.Services.AddOptions<DemoUserSettings>().BindConfiguration("DemoUser").ValidateDataAnnotations().ValidateOnStart();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(TrimAndChopStringsBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(DomainExceptionBehavior<,>));
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ResultLoggingBehavior<,>));

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtSettings = builder.Configuration.GetSection("Jwt").Get<JwtSettings>();
        var rsaKey = RsaKeyLoader.LoadPublicKey(jwtSettings!.PublicKeyPath);

        options.MapInboundClaims = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            IssuerSigningKey = rsaKey,
            NameClaimType = JwtRegisteredClaimNames.Sub
        };
    }
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevelopmentPolicy", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });

    options.AddPolicy("ProductionPolicy", policy =>
    {
        var clientSettings = builder.Configuration.GetSection("Client").Get<ClientSettings>();
        if (clientSettings != null && !string.IsNullOrEmpty(clientSettings.BaseUrl))
        {
            policy.WithOrigins(clientSettings.BaseUrl)
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        }
    });
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsProduction()) app.UseCors("ProductionPolicy");
else app.UseCors("DevelopmentPolicy");

app.UseAuthentication();

app.UseMiddleware<RequestMetadataMiddleware>();
app.UseMiddleware<RateLimitingMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();