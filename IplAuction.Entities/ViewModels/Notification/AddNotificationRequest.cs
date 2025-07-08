namespace IplAuction.Entities.ViewModels.Notification;

public class AddNotificationRequest
{
    public int UserId { get; set; } 
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
}
