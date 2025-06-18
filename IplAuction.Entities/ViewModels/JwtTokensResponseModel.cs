namespace IplAuction.Entities.ViewModels;

public class JwtTokensResponseModel
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}
