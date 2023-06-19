using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ottawa.Pigeon.Domain.Entities;

namespace Ottawa.Pigeon.Infrastructure.Configurations
{
    public class LanguageConfiguration : IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> entity)
        {
            entity.ToTable("language");

            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                   .IsFixedLength()
                   .HasMaxLength(2)
                   .IsUnicode(false)
                   .HasColumnName("code");

            entity.Property(e => e.Label)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("label");
            
            entity.HasData(
                new Language
                {
                    Code = "FR",
                    Label = "Français",
                },
                new Language
                {
                    Code = "EN",
                    Label = "English",
                }
            );

            entity.HasMany(u => u.Users)
                .WithOne(e => e.Language)
                .HasForeignKey(e => e.LanguageCode);
        }
    }
}
