using AutoFieldTranslationExperiment.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoFieldTranslationExperiment.Data;

public interface IApplicationDbContext
{
    DbSet<Product> Products { get; }
    
    DbSet<Translation> Translations { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}