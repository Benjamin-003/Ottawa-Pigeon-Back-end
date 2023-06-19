using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ottawa.Pigeon.Domain.Entities;

namespace Ottawa.Pigeon.Infrastructure.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> entity)
        {
            entity.ToTable("currency");

            entity.HasKey(e => e.Code);

            entity.Property(e => e.Code)
                   .IsFixedLength()
                   .HasMaxLength(3)
                   .IsUnicode(false)
                   .HasColumnName("code");

            entity.Property(e => e.Label)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("label");
            
            entity.Property(e => e.Flag)
                   .IsFixedLength()
                   .HasMaxLength(2)
                   .IsUnicode(false)
                   .HasColumnName("flag");

            entity.HasData(
                new Currency
                {
                    Code = "USD",
                    Label = "US Dollar",
                    Flag = "us",
                },
                new Currency
                {
                    Code = "EUR",
                    Label = "Euro",
                    Flag = "eu",
                }
            );

            entity.HasMany(u => u.Users)
                .WithOne(e => e.Currency)
                .HasForeignKey(e => e.CurrencyCode);
        }
    }
}
