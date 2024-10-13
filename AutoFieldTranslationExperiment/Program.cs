using AutoFieldTranslationExperiment.Data;
using AutoFieldTranslationExperiment.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("secrets.json", optional: false, reloadOnChange: true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ITranslationService, TranslationService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.Run();