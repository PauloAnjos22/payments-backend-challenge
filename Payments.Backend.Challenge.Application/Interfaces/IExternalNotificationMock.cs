using Payments.Backend.Challenge.Application.DTOs;

namespace Payments.Backend.Challenge.Application.Interfaces;

public interface IExternalNotificationMock
{
    Task<OperationResultDto> SendNotification();
}