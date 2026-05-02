using System.ComponentModel.DataAnnotations;

namespace DnTech_ECommerce.ViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres")]
        [Display(Name = "Nombre")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "La descripción no puede superar los 500 caracteres")]
        [Display(Name = "Descripción")]
        public string? Description { get; set; }

        [Display(Name = "Imagen")]
        public IFormFile? Image { get; set; }

        [Display(Name = "URL de imagen actual")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Activo")]
        public bool IsActive { get; set; } = true;
    }
}
