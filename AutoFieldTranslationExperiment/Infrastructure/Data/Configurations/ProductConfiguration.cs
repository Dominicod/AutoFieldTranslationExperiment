using AutoFieldTranslationExperiment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoFieldTranslationExperiment.Infrastructure.Data.Configurations;

internal sealed class ProductConfig : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasMany(i => i.Translations)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);
    }
}