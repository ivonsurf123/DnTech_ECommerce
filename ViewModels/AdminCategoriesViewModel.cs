namespace DnTech_ECommerce.ViewModels
{
    public class AdminCategoriesViewModel
    {
        // Lista de categorías
        public List<AdminCategorySummaryViewModel> Categories { get; set; } = new List<AdminCategorySummaryViewModel>();

        // Filtros
        public string? SearchTerm { get; set; }
        public bool? FilterIsActive { get; set; }

        // Paginación
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }
        public int PageSize { get; set; } = 20;
        public int TotalCategories { get; set; }

        // Estadísticas rápidas
        public int TotalActive { get; set; }
        public int TotalInactive { get; set; }
        public int TotalWithProducts { get; set; }
        public int TotalEmpty { get; set; }



    }
}
