using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

internal sealed class LanguageConfig : IEntityTypeConfiguration<Language>
{
    public void Configure(EntityTypeBuilder<Language> builder)
    {
        builder.Property(t => t.Code)
            .HasMaxLength(1000)
            .IsRequired();

        builder.HasMany(t => t.Translations)
            .WithOne(i => i.Language)
            .HasForeignKey(i => i.LanguageId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}