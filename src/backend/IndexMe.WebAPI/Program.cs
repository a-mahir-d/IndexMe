using IndexMe.Application;
using IndexMe.Infrastructure;
using IndexMe.Infrastructure.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOptions<DbSettings>().BindConfiguration("Db").ValidateDataAnnotations().ValidateOnStart();

builder.Services.AddApplication();
builder.Services.AddInfrastructure();

var app = builder.Build();

app.Run();