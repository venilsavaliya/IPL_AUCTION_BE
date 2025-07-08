namespace IplAuction.Entities.ViewModels.Notification;
public class NotificationResponse
{
    public NotificationResponse () {}

    public NotificationResponse(Models.Notification notification)
    {
        Id = notification.Id;
        Title = notification.Title;
        Message = notification.Message;
        CreatedAt = notification.CreatedAt;
        UserId = notification.UserId;
    } 
    public int Id { get; set; }
    public int UserId { get; set; }
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
