using AutoFieldTranslationExperiment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace AutoFieldTranslationExperiment.Infrastructure.Data;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }

    DbSet<Translation> Translations { get; }

    DbSet<Language> Languages { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    Task<IDbContextTransaction> BeginTransactionAsync();
}