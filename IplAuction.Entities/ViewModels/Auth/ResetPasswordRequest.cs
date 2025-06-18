using System.ComponentModel.DataAnnotations;

namespace IplAuction.Entities.DTOs.Auth;

public class ResetPasswordRequest
{
    public string Email { get; set; } = null!;

    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
    public string Password { get; set; } = null!;

    [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
    public string ConfirmPassword { get; set; } = null!;

    public string Token { get; set; } = null!;
}
