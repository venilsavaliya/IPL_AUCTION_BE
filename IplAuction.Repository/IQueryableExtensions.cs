namespace IplAuction.Repository;

using System.Linq.Expressions;
using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using Microsoft.EntityFrameworkCore;

public static class IQueryableExtensions
{
    public static async Task<PaginatedResult<TViewModel>> ToPaginatedListAsync<TEntity, TViewModel>(
       this IQueryable<TEntity> source,
       PaginationParams paginationParams,
       Expression<Func<TEntity, TViewModel>> selector)
    {
        var count = await source.CountAsync();
        var items = await source
            .Skip(paginationParams.Skip)
            .Take(paginationParams.PageSize)
            .Select(selector)
            .ToListAsync();

        return new PaginatedResult<TViewModel>
        {
            Items = items,
            TotalCount = count,
        };
    }
}

