namespace DnTech_ECommerce.ViewModels
{
    public class OrderListViewModel
    {
        public List<OrderSummaryViewModel> Orders { get; set; } = new();
        public int TotalOrders { get; set; }
    }
}
