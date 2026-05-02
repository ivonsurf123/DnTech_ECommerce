namespace DnTech_ECommerce.ViewModels
{
    public class FavoriteViewModel
    {
        public int FavoriteId { get; set; }
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal OriginalPrice { get; set; }
        public string? ImageUrl { get; set; }
        public bool IsOnSale { get; set; }
        public bool NotifyOnSale { get; set; }
        public DateTime DateAdded { get; set; }
        public bool PriceDropped { get; set; }
    }
}
