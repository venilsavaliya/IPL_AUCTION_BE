using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.User;

public class UpdateUserRequestModel : UserRequestModel
{
    public int Id { get; set; }
    public UserRole Role { get; set; }
    public string Username { get; set; } = string.Empty;
}
