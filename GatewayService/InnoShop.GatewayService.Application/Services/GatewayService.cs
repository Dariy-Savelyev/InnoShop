using InnoShop.GatewayService.Application.ServiceInterfaces;
using InnoShop.UserService.CrossCutting.Extensions;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace InnoShop.GatewayService.Application.Services;

public class GatewayService(IConfiguration configuration, HttpClient httpClient) : IGatewayService
{
    public async Task SendGatewayRequestAsync<TRequest>(HttpMethod method, string path, TRequest data, ClaimsPrincipal user)
    {
        var userId = user.FindFirst(ClaimTypes.Name)?.Value;

        var jsonString = JsonSerializer.Serialize(data);
        var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

        var baseUrl = configuration["ProductApi:BaseUrl"];
        var fullUrl = $"{baseUrl}/{path}";

        var request = new HttpRequestMessage(method, fullUrl)
        {
            Content = content
        };

        request.Headers.TryAddWithoutValidation("X-User-Id", userId);

        var response = await httpClient.SendAsync(request);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            throw ExceptionHelper.GetNotFoundException("Product not found.");
        }

        if (response.StatusCode == HttpStatusCode.Forbidden)
        {
            throw ExceptionHelper.GetForbiddenException("This user can't interact with this product.");
        }
    }
}