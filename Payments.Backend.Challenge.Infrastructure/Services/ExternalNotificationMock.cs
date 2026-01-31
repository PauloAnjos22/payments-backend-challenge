using System.Net.Http.Json;
using Microsoft.Extensions.Logging;
using Payments.Backend.Challenge.Application.DTOs;
using Payments.Backend.Challenge.Application.Interfaces;

namespace Payments.Backend.Challenge.Infrastructure.Services;

public class ExternalNotificationMock(HttpClient httpClient, ILogger<ExternalNotificationMock> logger) : IExternalNotificationMock
{
    public async Task<OperationResultDto> SendNotification()
    {
        var response = await httpClient.GetAsync("");
        if (response.IsSuccessStatusCode)
            return OperationResultDto.Ok();

        var result = await response.Content.ReadFromJsonAsync<NotificationMockDto>();
        return OperationResultDto.Fail($"Error sending notification {result?.Message}");
    }
}