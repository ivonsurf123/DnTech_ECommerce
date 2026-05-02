using DnTech_Ecommerce.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DnTech_ECommerce.Models
{
    public class Favorite
    {
        [Key]
        public int Id { get; set; }

        // Usuario
        [Required]
        public string UserId { get; set; } = string.Empty;
        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
          

        [DataType(DataType.DateTime)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        [DataType(DataType.DateTime)]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [Required]      
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceWhenAdded { get; set; }  // Track original price

        [Display(Name = "Notificaciones")]
        public bool NotifyOnSale { get; set; }        // Email notification preference

        // Relación con categoría
        [Display(Name = "Producto")]
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product? Product { get; set; }
    }
}
