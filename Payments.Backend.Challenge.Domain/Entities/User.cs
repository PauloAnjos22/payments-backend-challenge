using System.ComponentModel.DataAnnotations;
using Payments.Backend.Challenge.Domain.Enums;
using Payments.Backend.Challenge.Domain.Interfaces;

namespace Payments.Backend.Challenge.Domain.Entities;

public class User
{
    [Key]
    public long Id { get; private set; }
    public string FullName { get; private set; }
    public string? Cpf { get; private set; }    
    public string? Email { get; private set; } 
    public string? HashedPassword { get; private set; } 
    public UserType Type { get; private set; }

    private User(){}

    private User(string fullName, string cpf, string email, string hashedPassword, UserType type)
    {
        FullName = fullName;
        Cpf = cpf;
        Email = email;
        HashedPassword = hashedPassword;
        Type = type;
    }

    public User Create(
        string fullName, 
        string cpf, 
        string email, 
        string plainPassword, 
        UserType type, 
        IPasswordHasher passwordHasher)
    {
        if (string.IsNullOrEmpty(fullName))
            throw new ArgumentException("Name cannot be empty or null");

        if (string.IsNullOrEmpty(cpf) || string.IsNullOrWhiteSpace(cpf))
            throw new ArgumentException("Cpf cannot be empty or null");
        
        if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty or null.");
        
        if (string.IsNullOrEmpty(plainPassword) || string.IsNullOrWhiteSpace(plainPassword))
            throw new ArgumentException("Password cannot be empty or null.");

        if (!Enum.IsDefined(type))
            throw new ArgumentException("User type is not defined");

        var hashedPassword = passwordHasher.Hash(plainPassword);

        return new User(fullName, cpf, email, hashedPassword, type);
    }

    public void UserCanTransfer(UserType type)
    {
        if (type != UserType.Customer)
            throw new ArgumentException("Only customers can send money");
    }
}