using Microsoft.EntityFrameworkCore.Storage;
using Payments.Backend.Challenge.Application.Interfaces;

namespace Payments.Backend.Challenge.Infrastructure.Persistence;

public class EfUnitOfWork(AppDbContext context) : IEfUnitOfWork
{
    private IDbContextTransaction? _transaction;

    public async Task BeginTransactionAsync()
    {
        _transaction = await context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await context.SaveChangesAsync();
        await _transaction!.CommitAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _transaction!.RollbackAsync();
    }
}