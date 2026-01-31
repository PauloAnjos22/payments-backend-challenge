using System.ComponentModel.DataAnnotations;

namespace Payments.Backend.Challenge.Application.DTOs;

public class PaymentRequestDto
{
    [Required(ErrorMessage = "Payer id is required.")]
    public long Payer { get; init; }
    [Required(ErrorMessage = "Payee id is required.")]
    public long Payee { get; init; }
    [Required(ErrorMessage = "Payment value is required.")]
    public decimal Value { get; init; }
}