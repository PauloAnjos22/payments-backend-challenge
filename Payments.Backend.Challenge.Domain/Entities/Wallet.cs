using System.ComponentModel.DataAnnotations;

namespace Payments.Backend.Challenge.Domain.Entities;

public class Wallet
{
    [Key]
    public long Id { get; private set; }
    public long UserId { get; private set; } // fk
    public User? User { get; private set; } // navigation
    public decimal Balance { get; private set; }
    
    public Wallet(){}

    public Wallet(long userId, decimal balance)
    {
        UserId = userId;
        Balance = balance;
    }

    public static Wallet Create(long userId, decimal balance)
    {
        if (userId <= 0)
            throw new ArgumentException("UserId must be greater than zero.");
        if(balance < 0)
            throw new ArgumentException("Balance cannot be negative.");

        return new Wallet(userId, balance);
    }

    public void DebitBalance(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");
        if (Balance < amount)
            throw new ArgumentException("Insufficient balance.");

        Balance -= amount;
    }
    
    public void CreditBalance(decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");

        Balance += amount;
    }
}