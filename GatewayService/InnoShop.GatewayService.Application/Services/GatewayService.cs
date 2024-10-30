using InnoShop.GatewayService.Application.ServiceInterfaces;
using InnoShop.ProductService.CrossCutting.Enums;
using InnoShop.UserService.CrossCutting.Extensions;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace InnoShop.GatewayService.Application.Services;

public class GatewayService(IConfiguration configuration, HttpClient httpClient) : IGatewayService
{
    public async Task GetGatewayResponseStatusAsync<T>(HttpMethod method, string path, T data, ClaimsPrincipal user)
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

        request.Headers.TryAddWithoutValidation("User-Id", userId);

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

    public async Task<T> GetGatewayResponseDataAsync<T>(HttpMethod method, string path, string data, string dataName)
    {
        var baseUrl = configuration["ProductApi:BaseUrl"];
        var fullUrl = $"{baseUrl}/{path}";

        if (!string.IsNullOrEmpty(data))
        {
            fullUrl += $"?{dataName}={Uri.EscapeDataString(data)}";
        }

        var request = new HttpRequestMessage(method, fullUrl);

        var response = await httpClient.SendAsync(request);

        var contentString = await response.Content.ReadAsStringAsync();

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var deserializedContent = JsonSerializer.Deserialize<T>(contentString, jsonSerializerOptions);
        
        return deserializedContent!;
    }

    public async Task<T> GetGatewayResponseDataAsync<T>(HttpMethod method, string path, SortFieldEnum sortField, SortOrderEnum sortOrder)
    {
        var baseUrl = configuration["ProductApi:BaseUrl"];
        var fullUrl = $"{baseUrl}/{path}";

        fullUrl += $"?sortField={Uri.EscapeDataString(sortField.ToString())}&sortOrder={Uri.EscapeDataString(sortOrder.ToString())}";

        var request = new HttpRequestMessage(method, fullUrl);

        var response = await httpClient.SendAsync(request);

        var contentString = await response.Content.ReadAsStringAsync();

        var jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var deserializedContent = JsonSerializer.Deserialize<T>(contentString, jsonSerializerOptions);

        return deserializedContent!;
    }
}