using Payments.Backend.Challenge.Application.DTOs;
using Payments.Backend.Challenge.Application.Interfaces;
using Payments.Backend.Challenge.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using Payments.Backend.Challenge.Domain.Services;

namespace Payments.Backend.Challenge.Application.UseCases;

public class PaymentCoordinator(
    IUserRepository userRepository, 
    IWalletRepository walletRepository,
    IEfUnitOfWork efUnitOfWork,
    IExternalAuthorizationMock externalAuthorizationMock,
    IExternalNotificationMock externalNotificationMock,
    PaymentService paymentService,
    ILogger<PaymentCoordinator> logger) : IPaymentCoordinator
{
    public async Task<OperationResultDto> ExecuteAsync(PaymentRequestDto request)
    {
        try
        {
            var userPayer = await userRepository.GetUserByIdAsync(request.Payer);
            var userPayee = await userRepository.GetUserByIdAsync(request.Payee);
            if (userPayer == null || userPayee == null)
            {
                logger.LogError("Payer or payee not found."); // would be better improve it later 
                return OperationResultDto.Fail("Unable to process payment.");
            }

            var walletPayer = await walletRepository.GetWalletByUserIdAsync(request.Payer);
            var walletPayee = await walletRepository.GetWalletByUserIdAsync(request.Payee);
            if (walletPayer == null ||
                walletPayee ==
                null) // it shouldn’t be possible for a user to exist without a wallet, but I’ll check this, though
            {
                logger.LogError("Payer or payee wallet was not found.");
                return OperationResultDto.Fail("Unable to process payment.");
            }

            // Mock 
            var isAuthorized = await externalAuthorizationMock.IsAuthorizedAsync();
            if (!isAuthorized)
                return OperationResultDto.Fail("The payment is not authorized. PLease, try again later.");

            await efUnitOfWork.BeginTransactionAsync();

            paymentService.ExecutePayment(userPayer, walletPayer, walletPayee,
                request.Value); // domain concern execute the payment
            await efUnitOfWork.CommitTransactionAsync();

            var notification = await externalNotificationMock.SendNotificationAsync();
            if (!notification.Success)
                logger.LogError(notification.Error);

            return OperationResultDto.Ok();
        }
        catch (InvalidOperationException ex)
        {
            await efUnitOfWork.RollbackTransactionAsync();
            logger.LogWarning(ex, "Business rule violation during the payment.");
            return OperationResultDto.Fail(ex.Message);
        }
        catch (ArgumentException ex)
        {
            await efUnitOfWork.RollbackTransactionAsync();
            logger.LogWarning(ex, "Validation error during the payment.");
            return OperationResultDto.Fail(ex.Message);
        }
        catch (Exception ex)
        {
            await efUnitOfWork.RollbackTransactionAsync();
            logger.LogError(ex, "Payment has failed.");
            return OperationResultDto.Fail("Unexpected error occurred during the payment.");        
        }
    }
}