using Payments.Backend.Challenge.Application.DTOs;

namespace Payments.Backend.Challenge.Application.Interfaces;

public interface IRegisterUser
{
    Task<OperationResultDto<RegisterUserResponseDto>> ExecuteAsync(RegisterUserRequestDto request);
}