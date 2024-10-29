using System.Security.Claims;

namespace InnoShop.GatewayService.Application.ServiceInterfaces;

public interface IGatewayService : IBaseService
{
    Task SendGatewayRequestAsync<TRequest>(HttpMethod method, string path, TRequest data, ClaimsPrincipal user);
}