namespace Payments.Backend.Challenge.Application.DTOs;

public class OperationResultDto(bool success, string? error = null)
{
    public bool Success { get; } = success;
    public string? Error { get; } = error;

    public static OperationResultDto Ok() => new OperationResultDto(true);
    public static OperationResultDto Fail(string errorMessage) => new OperationResultDto(false, errorMessage);
}

public class OperationResultDto<T>(bool success, T? data, string? error = null) : OperationResultDto(success, error)
{
    public T? Data { get; } = data;

    public static OperationResultDto<T> Ok(T data) => new OperationResultDto<T>(true, data);
    public static OperationResultDto<T> Fail(string errorMessage) => new OperationResultDto<T>(false, default, errorMessage);
}