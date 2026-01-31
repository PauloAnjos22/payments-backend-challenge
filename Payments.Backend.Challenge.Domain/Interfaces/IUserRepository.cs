using Payments.Backend.Challenge.Domain.Entities;

namespace Payments.Backend.Challenge.Domain.Interfaces;

public interface IUserRepository
{
    Task AddAsync(User user);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> CpfExistsAsync(string cpf);
    Task<User?> GetUserByIdAsync(long id);
}