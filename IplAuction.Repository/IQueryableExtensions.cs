namespace IplAuction.Repository;

using IplAuction.Entities.DTOs;
using Microsoft.EntityFrameworkCore;

public static class IQueryableExtensions
{
     public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(
        this IQueryable<T> source,
        PaginationParams paginationParams)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip(paginationParams.Skip)
            .Take(paginationParams.PageSize)
            .ToListAsync();

        return new PaginatedResult<T>
        {
            Items = items,
            TotalCount = count,
            PageNumber = paginationParams.PageNumber,
            PageSize = paginationParams.PageSize
        };
    }
}

