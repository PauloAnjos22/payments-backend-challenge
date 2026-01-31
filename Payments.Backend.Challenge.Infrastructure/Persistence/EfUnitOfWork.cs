using Microsoft.EntityFrameworkCore.Storage;
using Payments.Backend.Challenge.Application.Interfaces;

namespace Payments.Backend.Challenge.Infrastructure.Persistence;

public class EfUnitOfWork(AppDbContext context, IDbContextTransaction transaction) : IEfUnitOfWork
{
    public async Task BeginTransactionAsync()
    {
        await context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await transaction.CommitAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await transaction.RollbackAsync();
    }
}