namespace Payments.Backend.Challenge.Application.DTOs;

public class AuthorizationMockDto
{
    public string? Status { get; init; }
    public AuthorizationData? Data { get; init; }
}

public class AuthorizationData
{
    public bool Authorization { get; set; }
}