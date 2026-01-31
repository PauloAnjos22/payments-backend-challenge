using Microsoft.AspNetCore.Identity;
using Payments.Backend.Challenge.Domain.Interfaces;

namespace Payments.Backend.Challenge.Infrastructure.Security;

internal sealed class FakeUserForHashingPassword
{ 
    public Guid Id = Guid.NewGuid();
}

public class AspNetIdentityPasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<FakeUserForHashingPassword> _hasher = new();
    
    public string Hash(string plainPassword)
    {
        var user = new FakeUserForHashingPassword();
        return _hasher.HashPassword(user, plainPassword);
    }
}