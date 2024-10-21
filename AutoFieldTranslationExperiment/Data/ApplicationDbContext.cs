using System.Reflection;
using AutoFieldTranslationExperiment.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoFieldTranslationExperiment.Data;

internal sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), IApplicationDbContext
{
    public DbSet<Product> Products => Set<Product>();
    
    public DbSet<Translation> Translations => Set<Translation>();
    
    public DbSet<Language> Languages => Set<Language>();
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Adds all IEntityTypeConfiguration<TEntity> classes from the executing assembly
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}