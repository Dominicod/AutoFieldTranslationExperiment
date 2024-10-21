using AutoFieldTranslationExperiment.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoFieldTranslationExperiment.Data.Configurations;

internal sealed class LanguageConfig : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.Property(t => t.Code)
            .HasMaxLength(1000)
            .IsRequired();
    }
}