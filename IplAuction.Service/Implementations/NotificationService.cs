using IplAuction.Entities.Hubs;
using IplAuction.Entities.Models;
using IplAuction.Entities.ViewModels.Notification;
using IplAuction.Repository.Interfaces;
using IplAuction.Service.Interface;
using Microsoft.AspNetCore.SignalR;

namespace IplAuction.Service.Implementations;

public class NotificationService(IHubContext<NotificationHub> hubContext, INotificationRepository notificationRepository) : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext = hubContext;

    private readonly INotificationRepository _notificationRepository = notificationRepository;

    public async Task AddNotification(AddNotificationRequest request)
    {
        Notification notification = new()
        {
            UserId = request.UserId,
            Title = request.Title,
            Message = request.Message
        };

        await _notificationRepository.AddAsync(notification);

        await _notificationRepository.SaveChangesAsync();
    }

    public async Task<List<NotificationResponse>> GetUnreadNotification(int userId)
    {
        List<NotificationResponse> notifications = await _notificationRepository.GetAllWithFilterAsync(n => n.UserId == userId && n.IsRead != true, n => new NotificationResponse(n));
        return notifications;
    }

    public async Task SendNotificationToUserAsync(string userId, object message)
    {
        await _hubContext.Clients.User(userId).SendAsync("ReceiveNotification", message);
    }

    public async Task MarkAllNotificationAsRead(int userId)
    {
        List<Notification> notifications = await _notificationRepository.GetAllWithFilterAsync(u => u.IsDeleted != true && u.UserId == userId);

        foreach (var n in notifications)
        {
            n.IsRead = true;
        }

        await _notificationRepository.SaveChangesAsync();
    }
}
