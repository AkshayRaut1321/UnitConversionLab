using UnitConversion.Api.Repositories;
using UnitConversion.Core.Interfaces;
using UnitConversion.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "Unit Conversion API";
});

builder.Services.AddSingleton<IUnitRepository, JsonUnitRepository>();
builder.Services.AddScoped<IConversionService, ConversionService>();
builder.Services.AddScoped<IUnitConverter, LinearUnitConverter>();
builder.Services.AddScoped<IUnitConverter, TemperatureConverter>();

var app = builder.Build();

app.UseOpenApi();
app.UseSwaggerUi();

app.MapControllers();

app.Run();