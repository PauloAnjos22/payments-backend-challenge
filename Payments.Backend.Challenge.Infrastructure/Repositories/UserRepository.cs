using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Payments.Backend.Challenge.Domain.Entities;
using Payments.Backend.Challenge.Domain.Interfaces;
using Payments.Backend.Challenge.Infrastructure.Persistence;

namespace Payments.Backend.Challenge.Infrastructure.Repositories;

public class UserRepository(AppDbContext context, ILogger<UserRepository> logger) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        try
        {
            await context.AddAsync(user);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating User");
            throw;
        }
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
        return await context.Users.AnyAsync(e => e.Email == email);
    }

    public async Task<bool> CpfExistsAsync(string cpf)
    {
        return await context.Users.AnyAsync(c => c.Cpf == cpf);
    }

    public async Task<User?> GetUserByIdAsync(long id)
    {
        return await context.Users.FindAsync(id);
    }
}