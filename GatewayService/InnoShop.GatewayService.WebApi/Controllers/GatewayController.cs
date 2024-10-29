using InnoShop.GatewayService.Application.ServiceInterfaces;
using InnoShop.ProductService.Application.Models;
using InnoShop.UserService.CrossCutting.Constants;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.GatewayService.WebApi.Controllers;

public class GatewayController(IGatewayService gatewayService) : BaseController
{
    [HttpPost]
    public async Task SendGatewayProductCreateRequest(ProductCreationModel model)
    {
        await gatewayService.SendGatewayRequestAsync(HttpMethod.Post, PathConstants.ProductCreatePath, model, User);
    }

    [HttpPut]
    public async Task SendGatewayProductEditRequest(ProductModificationModel model)
    {
        await gatewayService.SendGatewayRequestAsync(HttpMethod.Put, PathConstants.ProductEditPath, model, User);
    }

    [HttpDelete]
    public async Task SendGatewayProductDeleteRequest(ProductDeletionModel model)
    {
        await gatewayService.SendGatewayRequestAsync(HttpMethod.Delete, PathConstants.ProductDeletePath, model, User);
    }
}