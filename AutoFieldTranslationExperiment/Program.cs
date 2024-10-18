using System.Text.Json.Serialization;
using AutoFieldTranslationExperiment.Data;
using AutoFieldTranslationExperiment.Middleware;
using AutoFieldTranslationExperiment.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("secrets.json", optional: false, reloadOnChange: true);

builder.Host.UseSerilog((ctx, _, loggerConfiguration) =>
{
    loggerConfiguration
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
        .WriteTo.Async(wt => wt.Console())
        .ReadFrom.Configuration(ctx.Configuration);
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ITranslationService, TranslationService>();
builder.Services.AddScoped<RequestInformationMiddleware>();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

var app = builder.Build();

app.UseSerilogRequestLogging();
app.UseMiddleware<RequestInformationMiddleware>();
app.MapControllers();
app.UseHttpsRedirection();
app.MapGet("/api/health", () => "OK");
app.Run();