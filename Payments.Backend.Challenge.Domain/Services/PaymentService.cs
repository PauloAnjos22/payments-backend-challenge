using Payments.Backend.Challenge.Domain.Entities;

namespace Payments.Backend.Challenge.Domain.Services;

public class PaymentService
{
    public void ExecutePayment(
        User payer,
        Wallet walletPayer,
        Wallet walletPayee,
        decimal value)
    {
        payer.UserCanTransfer(payer.Type);

        if (walletPayer.UserId == walletPayee.UserId)
            throw new InvalidOperationException("Payer and payee cannot be the same.");
        
        walletPayer.DebitBalance(value);
        walletPayee.CreditBalance(value);
    }
}