using AutoFieldTranslationExperiment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoFieldTranslationExperiment.Data.Configurations;

internal sealed class TranslationConfig : IEntityTypeConfiguration<Translation>
{
    public void Configure(EntityTypeBuilder<Translation> builder)
    {
        builder.Property(t => t.LanguageCode)
            .HasMaxLength(10)
            .IsRequired();
        
        builder.Property(t => t.Key)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(t => t.Value)
            .HasMaxLength(1000);
    }
}