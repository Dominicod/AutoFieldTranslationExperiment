using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoFieldTranslationExperiment.Infrastructure.Data.Configurations;

internal sealed class TranslationConfig : IEntityTypeConfiguration<Translation>
{
    public void Configure(EntityTypeBuilder<Translation> builder)
    {
        builder.Property(t => t.Key)
            .HasMaxLength(1000)
            .IsRequired();

        builder.Property(t => t.Value)
            .HasMaxLength(1000);
    }
}