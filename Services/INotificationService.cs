using DnTech_ECommerce.Models;

namespace DnTech_ECommerce.Services
{
    public interface INotificationService
    {
        Task<Notification> CreateAndSendNotification(
            string userId,
            string message,
            NotificationType type,
            string? link = null,
            int? orderId = null
        );
    }
}
