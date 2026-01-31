namespace Payments.Backend.Challenge.Application.Interfaces;

public interface IEfUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}