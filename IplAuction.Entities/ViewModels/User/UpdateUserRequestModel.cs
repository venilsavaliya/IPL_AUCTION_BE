using IplAuction.Entities.Enums;

namespace IplAuction.Entities.ViewModels.User;

public class UpdateUserRequestModel : AddUserRequestModel
{
    public int Id { get; set; }
    public UserRole Role { get; set; } 
}
