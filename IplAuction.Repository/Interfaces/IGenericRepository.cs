using System.Linq.Expressions;
using IplAuction.Entities;
using IplAuction.Entities.DTOs;

namespace IplAuction.Repository.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<List<T>> GetAllWithFilterAsync(Expression<Func<T, bool>> filter);
    IQueryable<T> GetAllQueryableWithFilterAsync(Expression<Func<T, bool>> filter);
    Task<T?> GetWithFilterAsync(Expression<Func<T, bool>> filter,params Expression<Func<T, object>>[] includes);
    Task<T1?> GetWithFilterAsync<T1>(Expression<Func<T, bool>> filter, Expression<Func<T, T1>> selector);
    Task<List<T1>> GetAllWithFilterAsync<T1>(Expression<Func<T, bool>> filter, Expression<Func<T, T1>> selector);
    Task<List<T>> GetEagerLoadAllWithFilterAsync(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetAllAsync();
    void RemoveRange(IEnumerable<T> entities);
    Task<T?> FindAsync(int id);
    Task AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);
    void Update(T entity);
    void Delete(T entity);
    Task<bool> SaveChangesAsync();
}

