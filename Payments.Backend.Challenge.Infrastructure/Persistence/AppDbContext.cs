using Microsoft.EntityFrameworkCore;
using Payments.Backend.Challenge.Domain.Entities;

namespace Payments.Backend.Challenge.Infrastructure.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
}