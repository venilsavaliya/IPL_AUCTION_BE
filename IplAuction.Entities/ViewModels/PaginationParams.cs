namespace IplAuction.Entities;

public class PaginationParams
{
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    private const int MaxPageSize = 50;
    public string? SortBy { get; set; }
    public string? SortDirection { get; set; } = "asc";

    public int Skip => (PageNumber - 1) * PageSize;

    public void Validate()
    {
        if (PageSize > MaxPageSize)
            PageSize = MaxPageSize;
    }
}
