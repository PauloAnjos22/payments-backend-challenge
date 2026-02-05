using Microsoft.Extensions.Logging;
using Payments.Backend.Challenge.Application.DTOs;
using Payments.Backend.Challenge.Application.Interfaces;
using Payments.Backend.Challenge.Domain.Entities;
using Payments.Backend.Challenge.Domain.Interfaces;
using Payments.Backend.Challenge.Domain.Services;

namespace Payments.Backend.Challenge.Application.UseCases;

public class RegisterUser(
    IUserRepository userRepository, 
    IWalletRepository walletRepository,
    UserUniquesPolicy userUniquesPolicy,
    IPasswordHasher passwordHasher,
    ILogger<RegisterUser> logger) : IRegisterUser
{
    public async Task<OperationResultDto<RegisterUserResponseDto>> ExecuteAsync(RegisterUserRequestDto request)
    {
        try
        {
            var cpfExist = await userRepository.CpfExistsAsync(request.Cpf!);
            userUniquesPolicy.EnsureCpfIsUnique(cpfExist);

            var emailExist = await userRepository.EmailExistsAsync(request.Email!);
            userUniquesPolicy.EnsureEmailIsUnique(emailExist);

            var newUser = User.Create(request.FullName!, request.Cpf!, request.Email!, request.Password!, request.Type,
                passwordHasher);
            await userRepository.AddAsync(newUser);

            var newWallet = Wallet.Create(newUser.Id, 15);
            await walletRepository.AddAsync(newWallet);

            var response = new RegisterUserResponseDto
            {
                UserId = newUser.Id.ToString(),
                WalletId = newWallet.Id.ToString(),
            };

            return OperationResultDto<RegisterUserResponseDto>.Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            logger.LogWarning(ex, "Business rule violation while registering user.");
            return OperationResultDto<RegisterUserResponseDto>.Fail(ex.Message);
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Validation error while registering user.");
            return OperationResultDto<RegisterUserResponseDto>.Fail(ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error while registering user.");
            return OperationResultDto<RegisterUserResponseDto>.Fail("An unexpected error ocurred. Please try again later.");
        }
    }
}