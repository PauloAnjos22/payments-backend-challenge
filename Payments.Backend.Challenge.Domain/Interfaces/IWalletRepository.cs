using Payments.Backend.Challenge.Domain.Entities;

namespace Payments.Backend.Challenge.Domain.Interfaces;

public interface IWalletRepository
{
    Task AddAsync(Wallet wallet);
    Task<Wallet?> GetWalletByUserIdAsync(long userId);
}