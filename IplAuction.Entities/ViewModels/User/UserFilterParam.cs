using IplAuction.Entities;

namespace IplAuction.Entities.ViewModels.User;

public class UserFilterParam : PaginationParams
{
    public string? Search { get; set; }
    public string? Role { get; set; }
    
}
