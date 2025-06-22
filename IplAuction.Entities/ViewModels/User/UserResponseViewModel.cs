using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.User;

public class UserResponseViewModel
{
    public int Id { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string Role { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
}
