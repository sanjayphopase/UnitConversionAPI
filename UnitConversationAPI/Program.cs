using UnitConversationAPI.Interfaces;
using UnitConversationAPI.Middlewares;
using UnitConversationAPI.Services;
using UnitConversationAPI.Strategies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IConversionService, ConversionService>();

builder.Services.AddScoped<IConversionStrategy, LengthConversionStrategy>();
builder.Services.AddScoped<IConversionStrategy, TemperatureConversionStrategy>();
builder.Services.AddScoped<IConversionStrategy, WeightConversionStrategy>();

builder.Services.AddScoped<IConversionStrategyEngine, ConversionStrategyEngine>();


var app = builder.Build();

app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
