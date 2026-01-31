using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payments.Backend.Challenge.Domain.Entities;
using Payments.Backend.Challenge.Domain.Interfaces;
using Payments.Backend.Challenge.Infrastructure.Persistence;

namespace Payments.Backend.Challenge.Infrastructure.Repositories;

public class WalletRepository(AppDbContext context, ILogger<UserRepository> logger) : IWalletRepository
{
    public async Task AddAsync(Wallet wallet)
    {
        try
        {
            await context.AddAsync(wallet);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating wallet.");
            throw;
        }
    }

    public async Task<Wallet?> GetWalletByUserIdAsync(long userId)
    {
        return await context.Wallets.FirstOrDefaultAsync(w => w.UserId == userId);
    }
}