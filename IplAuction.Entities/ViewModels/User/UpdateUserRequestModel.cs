using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.User;

public class UpdateUserRequestModel
{
    public int Id { get; set; }
    public UserRole Role { get; set; }

    public string Username { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;
}
