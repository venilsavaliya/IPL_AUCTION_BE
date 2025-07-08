using IplAuction.Entities.ViewModels.Notification;

namespace IplAuction.Service.Interface;

public interface INotificationService
{
    Task SendNotificationToUserAsync(string userId, object message);

    Task AddNotification(AddNotificationRequest request);

    Task<List<NotificationResponse>> GetUnreadNotification(int userId);

    Task MarkAllNotificationAsRead(int userId);
}
