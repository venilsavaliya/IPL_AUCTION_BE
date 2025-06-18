using IplAuction.Entities.DTOs;

namespace IplAuction.Entities.ViewModels.User;

public class UserFilterModel
{
    public string? Search { get; set; }
    public string? Role { get; set; }
    public bool? IsActive { get; set; }

    // Pagination
    public PaginationParams Pagination { get; set; } = new();
}
