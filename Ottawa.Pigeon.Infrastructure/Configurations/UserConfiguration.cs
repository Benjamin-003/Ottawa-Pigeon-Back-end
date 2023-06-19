using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ottawa.Pigeon.Domain.Entities;

namespace Ottawa.Pigeon.Infrastructure.Configurations
{
    /// <summary>
    /// Configuration pour mapper l'entité User dans la table Users
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable("user");

            entity.Property(e => e.Id).HasColumnName("id");

            entity.Property(e => e.Surname)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");

            entity.Property(e => e.Firstname)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("firstname");

            entity.Property(e => e.Birthdate)
                   .HasColumnType("date")
                   .HasColumnName("birth_date");

            entity.Property(e => e.Address)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("address");

            entity.Property(e => e.Zipcode)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("zip_code");

            entity.Property(e => e.City)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("city");

            entity.Property(e => e.Country)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("country");

            entity.Property(e => e.Mail)
                   .HasMaxLength(50)
                   .IsUnicode(false)
                   .HasColumnName("mail");

            entity.HasIndex(e => e.Mail)
                    .IsUnique();

            entity.Property(e => e.Password)
                   .HasMaxLength(200)
                   .IsUnicode(false)
                   .HasColumnName("password");

            entity.Property(e => e.Newsletter)
                   .HasColumnName("newsletter");

            entity.Property(e => e.LanguageCode)
                .HasColumnName("language_code")
                .IsUnicode(false)
                .HasDefaultValue("FR");
            
            entity.Property(e => e.CurrencyCode)
                .HasColumnName("currency_code")
                .IsUnicode(false)
                .HasDefaultValue("USD");

            entity.Property(e => e.SubscriptionCode)
                .HasColumnName("subscription_code")
                .IsUnicode(false);
        }
    }
}
