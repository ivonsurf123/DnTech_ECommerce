using DnTech_ECommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnTech_ECommerce.Data.Configurations
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            // Primary key
            builder.HasKey(f => f.Id);

            // Properties

            builder.Property(f => f.PriceWhenAdded)
                .HasPrecision(18, 2);

            builder.Property(f => f.CreatedAt)
                .IsRequired();

            builder.Property(f => f.NotifyOnSale)
                .IsRequired();

            // Relación con User
            builder.HasOne(f => f.User)
                   .WithMany()
                   .HasForeignKey(f => f.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Relacion con Producto
            builder.HasOne(f => f.Product)
                   .WithMany()
                   .HasForeignKey(f => f.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Indexes for performance
            builder.HasIndex(f => new { f.UserId, f.ProductId })
                .IsUnique()
                .HasDatabaseName("IX_Customer_Product");
        }
    }
}
