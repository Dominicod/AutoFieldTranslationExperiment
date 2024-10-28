using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure;

public interface IApplicationDbContext
{
    DbSet<T> Set<T>() where T : class;
    
    DbSet<Product> Products { get; }

    DbSet<Translation> Translations { get; }

    DbSet<Language> Languages { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    
    Task<IDbContextTransaction> BeginTransactionAsync();
}