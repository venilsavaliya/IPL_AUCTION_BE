namespace IplAuction.Service.Interface;

public interface ICurrentUserService
{
    int UserId { get; }
    string? Email { get; }
    string? Role { get; }
}
