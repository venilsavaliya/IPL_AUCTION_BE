namespace IplAuction.Entities.ViewModels.Auth;

public class RefreshTokenResponse
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}
