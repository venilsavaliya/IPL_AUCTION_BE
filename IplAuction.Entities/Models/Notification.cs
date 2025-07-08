namespace IplAuction.Entities.Models;

public class Notification
{
    public int Id { get; set; }
    public int UserId { get; set; } 
    public string Title { get; set; } = null!;
    public string Message { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;
    public bool IsDeleted { get; set; } = false;

    public User User { get; set; } = null!;
}
