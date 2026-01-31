using Payments.Backend.Challenge.Application.DTOs;

namespace Payments.Backend.Challenge.Application.Interfaces;

public interface IAuthorizationMock
{
    Task<bool> IsAuthorizedAsync();
}