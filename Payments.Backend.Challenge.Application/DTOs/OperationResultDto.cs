namespace Payments.Backend.Challenge.Application.DTOs;

public class OperationResultDto(bool success, string? error = null)
{
    public bool Success { get; private set; } = success;
    public string? Error { get; private set; } = error;

    public static OperationResultDto Ok() => new OperationResultDto(true);
    public static OperationResultDto Fail(string errorMessage) => new OperationResultDto(false, errorMessage);
}