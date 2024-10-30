using InnoShop.ProductService.CrossCutting.Enums;
using System.Security.Claims;

namespace InnoShop.GatewayService.Application.ServiceInterfaces;

public interface IGatewayService : IBaseService
{
    Task GetGatewayResponseStatusAsync<T>(HttpMethod method, string path, T data, ClaimsPrincipal user);
    Task<T> GetGatewayResponseDataAsync<T>(HttpMethod method, string path, string data, string dataName);
    Task<T> GetGatewayResponseDataAsync<T>(HttpMethod method, string path, SortFieldEnum sortField, SortOrderEnum sortOrder);
}