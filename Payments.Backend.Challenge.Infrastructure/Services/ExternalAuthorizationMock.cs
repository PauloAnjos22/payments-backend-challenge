using System.Net.Http.Json;
using Payments.Backend.Challenge.Application.DTOs;
using Payments.Backend.Challenge.Application.Interfaces;

namespace Payments.Backend.Challenge.Infrastructure.Services;

public class ExternalAuthorizationMock(HttpClient httpClient) : IExternalAuthorizationMock
{
    public async Task<bool> IsAuthorizedAsync()
    {
        var response = await httpClient.GetAsync("");
        if (!response.IsSuccessStatusCode)
            return false;

        var result = await response.Content.ReadFromJsonAsync<AuthorizationMockDto>();
        return result?.Data?.Authorization == true;
    }
}