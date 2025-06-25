using IplAuction.Entities.Enums;
using Microsoft.AspNetCore.Http;

namespace IplAuction.Entities.ViewModels.User;

public class UserRequestModel
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string Password { get; set; } = string.Empty;
    public IFormFile? Image { get; set; }
    public UserGender Gender { get; set; }
    public string MobileNumber { get; set; } = null!;
}

public class AddUserRequestModel : UserRequestModel
{
    public UserRole Role { get; set; }
}
