using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ottawa.Pigeon.Domain.Entities;

namespace Ottawa.Pigeon.Infrastructure.Configurations
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> entity)
        {
            entity.ToTable("subscription");

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
            
            entity.Property(e => e.IsDefault)
                .IsUnicode(false)
                .HasColumnName("isDefault");

            entity.HasData(
                new Subscription
                {
                    Code = "BA",
                    Label = "Basique",
                    IsDefault = true,
                }, 
                new Subscription
                {
                    Code = "ES",
                    Label = "Essentiel",
                    IsDefault = false,
                }, 
                new Subscription
                {
                    Code = "PR",
                    Label = "Premium",
                    IsDefault = false,
                }
             );

            entity.HasMany(u => u.Users)
                .WithOne(u => u.Subscription)
                .HasForeignKey(u => u.SubscriptionCode);
        }
    }
}
