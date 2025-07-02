using IplAuction.Entities;
using IplAuction.Repository.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace IplAuction.Repository.Implementations;

public class UnitOfWork(IplAuctionDbContext context) : IUnitOfWork
{
    private readonly IplAuctionDbContext _context = context;
    private IDbContextTransaction? _transaction;

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        await _transaction?.CommitAsync()!;
    }

    public async Task RollbackAsync()
    {
        await _transaction?.RollbackAsync()!;
    }
}
