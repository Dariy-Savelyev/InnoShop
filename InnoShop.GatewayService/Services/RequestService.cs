using InnoShop.UserService.CrossCutting.Extensions;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace InnoShop.GatewayService.Services;

public class RequestService(IConfiguration configuration, HttpClient httpClient)
{
    public async Task SendGatewayRequestAsync<TRequest>(HttpMethod method, string path, TRequest data, ClaimsPrincipal user)
    {
        var headers = user.Claims.ToDictionary(claim => $"X-User-{claim.Type}", claim => claim.Value);

        var jsonString = JsonSerializer.Serialize(data);
        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

        var baseUrl = configuration["ProductApi:BaseUrl"];
        var fullUrl = $"{baseUrl}/{path}";

        var request = new HttpRequestMessage(method, fullUrl)
        {
            Content = content
        };

        foreach (var header in headers)
        {
            request.Headers.TryAddWithoutValidation(header.Key, header.Value);
        }

        var response = await httpClient.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw ExceptionHelper.GetNotFoundException("Product not found.");
        }
    }
}