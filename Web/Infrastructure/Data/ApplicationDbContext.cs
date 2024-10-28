using System.Reflection;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AutoFieldTranslationExperiment.Infrastructure.Data;

internal sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : DbContext(options), IApplicationDbContext
{
    public DbSet<Product> Products => Set<Product>();

    public DbSet<Translation> Translations => Set<Translation>();

    public DbSet<Language> Languages => Set<Language>();
    
    public Task<IDbContextTransaction> BeginTransactionAsync() => Database.BeginTransactionAsync();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Adds all IEntityTypeConfiguration<TEntity> classes from the executing assembly
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}