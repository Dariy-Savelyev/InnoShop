using InnoShop.GatewayService.Services;
using InnoShop.ProductService.Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.UserService.WebApi.Controllers;

public class GatewayController(RequestService gatewayService) : BaseController
{
    [HttpPost]
    public async Task SendGatewayProductCreateRequest(ProductCreationModel model, string path)
    {
        await gatewayService.SendGatewayRequestAsync(HttpMethod.Post, path, model, User);
    }

    [HttpPut]
    public async Task SendGatewayProductEditRequest(ProductModificationModel model, string path)
    {
        await gatewayService.SendGatewayRequestAsync(HttpMethod.Put, path, model, User);
    }

    [HttpDelete]
    public async Task SendGatewayProductDeleteRequest(ProductDeletionModel model, string path)
    {
        await gatewayService.SendGatewayRequestAsync(HttpMethod.Delete, path, model, User);
    }
}