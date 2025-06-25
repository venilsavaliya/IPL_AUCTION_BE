using IplAuction.Entities.Enums;
using IplAuction.Entities.Models;


namespace IplAuction.Entities.ViewModels.User;

public class UserResponseViewModel
{
    public UserResponseViewModel()
    {
        
    }
    public UserResponseViewModel(Models.User user)
    {
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        DateOfBirth = user.DateOfBirth;
        Role = user.Role;
        Image = user.Image;
        Gender = user.Gender;
        MobileNumber = user.MobileNumber;
    }
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public UserRole Role { get; set; }
    public string? Image { get; set; }
    public UserGender Gender { get; set; }
    public string MobileNumber { get; set; } = null!;
}
