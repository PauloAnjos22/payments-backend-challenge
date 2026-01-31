namespace Payments.Backend.Challenge.Domain.Interfaces;

public interface IPasswordHasher
{
    string Hash(string plainPassword);
}