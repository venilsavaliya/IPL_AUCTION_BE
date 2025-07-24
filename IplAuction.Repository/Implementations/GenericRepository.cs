using System.Linq.Expressions;
using IplAuction.Entities;
using IplAuction.Entities.DTOs;
using IplAuction.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace IplAuction.Repository.Implementations;

public class GenericRepository<T>(IplAuctionDbContext context) : IGenericRepository<T> where T : class
{
    protected readonly IplAuctionDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<List<T>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    // public async Task<PaginatedResult<T>> GetPagedAsync(PaginationParams paginationParams)
    // {
    //     return await _dbSet.AsQueryable().ToPaginatedListAsync<T,>(paginationParams);
    // }

    public async Task<T?> GetWithFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.FirstOrDefaultAsync(filter);
    }

    public async Task<T1?> GetWithFilterAsync<T1>(Expression<Func<T, bool>> filter, Expression<Func<T, T1>> selector)
    {
        return await _dbSet.Where(filter).Select(selector).FirstOrDefaultAsync();
    }

    public async Task<List<T>> GetAllWithFilterAsync(Expression<Func<T, bool>> filter)
    {
        return await _dbSet.Where(filter).ToListAsync();
    }

    public async Task<List<T>> GetEagerLoadAllWithFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;

        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.Where(filter).ToListAsync();
    }

    public IQueryable<T> GetAllQueryableWithFilterAsync(Expression<Func<T, bool>> filter)
    {
        return _dbSet.Where(filter);
    }



    public async Task<List<T1>> GetAllWithFilterAsync<T1>(Expression<Func<T, bool>> filter, Expression<Func<T, T1>> selector)
    {
        return await _dbSet.Where(filter).Select(selector).ToListAsync();
    }

    public async Task<T?> FindAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        _dbSet.RemoveRange(entities);
    }



    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }


}

