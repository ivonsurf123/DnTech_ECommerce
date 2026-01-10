using DnTech_ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnTech_ECommerce.Data.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.FullName)
    .IsRequired()
    .HasMaxLength(200);

            builder.Property(u => u.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(u => u.City)
                .HasMaxLength(50);

            builder.Property(u => u.ZipCode)
                .HasMaxLength(10);

            builder.Property(u => u.Country)
                .HasMaxLength(50);

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            // Indexes
            builder.HasIndex(u => u.Email)
                .IsUnique();

            builder.HasIndex(u => u.IsActive);
        }
    }
}
