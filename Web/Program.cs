using System.Text.Json.Serialization;
using AutoFieldTranslationExperiment.Exceptions;
using AutoFieldTranslationExperiment.Infrastructure;
using AutoFieldTranslationExperiment.Infrastructure.Data;
using AutoFieldTranslationExperiment.Middleware;
using AutoFieldTranslationExperiment.Services;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("secrets.json", false, true);

builder.Host.UseSerilog((ctx, _, loggerConfiguration) =>
{
    loggerConfiguration
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Environment", builder.Environment.EnvironmentName)
        .WriteTo.Async(wt => wt.Console())
        .ReadFrom.Configuration(ctx.Configuration);
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web", Version = "v1" });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ILanguageService, LanguageService>();
builder.Services.AddScoped<LanguageInformation>();
builder.Services.AddScoped<ITranslationService, TranslationService>();
builder.Services.AddScoped<RequestInformationMiddleware>();
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();
app.UseMiddleware<RequestInformationMiddleware>();
app.MapControllers();
app.UseHttpsRedirection();
app.UseExceptionHandler(_ => { });
app.MapGet("/api/health", () => "OK");
app.Run();