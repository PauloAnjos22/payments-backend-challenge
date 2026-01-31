using Payments.Backend.Challenge.Application.DTOs;

namespace Payments.Backend.Challenge.Application.Interfaces;

public interface IPaymentCoordinator
{
    Task<OperationResultDto> ExecuteAsync(PaymentRequestDto request);
}