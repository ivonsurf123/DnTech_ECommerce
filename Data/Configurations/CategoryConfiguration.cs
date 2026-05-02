using DnTech_Ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DnTech_ECommerce.Data.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        void IEntityTypeConfiguration<Category>.Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.Property(c => c.Slug)
                .HasMaxLength(150);

            builder.Property(c => c.ImageUrl)
                .HasMaxLength(500);

            //builder.Property(c => c.IsActive)
               // .IsRequired();

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.HasIndex(c => c.Slug).IsUnique();
            builder.HasIndex(c => c.Name).IsUnique(); 
            //builder.HasIndex(c => c.IsActive);

            // Relaciones
            builder.HasMany(c => c.Products)
                   .WithOne(p => p.Category)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            // Seed Data
            var seedDate = new DateTime(2025, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            builder.HasData(
                new Category
                {
                    Id = 1,
                    Name = "Smartphones",
                    Description = "Teléfonos inteligentes",
                    Slug = "smartphones",
                    IsActive = true,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = 2,
                    Name = "Laptops",
                    Description = "Computadoras portátiles",
                    Slug = "laptops",
                    IsActive = true,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = 3,
                    Name = "Audio",
                    Description = "Auriculares y altavoces",
                    Slug = "audio",
                    IsActive = true,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = 4,
                    Name = "Wearables",
                    Description = "Dispositivos portátiles",
                    Slug = "wearables",
                    IsActive = true,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = 5,
                    Name = "Tablets",
                    Description = "Tabletas electrónicas",
                    Slug = "tablets",
                    IsActive = true,
                    CreatedAt = seedDate
                },
                new Category
                {
                    Id = 6,
                    Name = "Accesorios",
                    Description = "Accesorios para dispositivos",
                    Slug = "accesorios",
                    IsActive = true,
                    CreatedAt = seedDate
                }
            );
        }
    }
}
