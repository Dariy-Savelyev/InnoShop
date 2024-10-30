using InnoShop.GatewayService.Application.ServiceInterfaces;
using InnoShop.ProductService.Application.Models;
using InnoShop.ProductService.CrossCutting.Enums;
using InnoShop.UserService.CrossCutting.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.GatewayService.WebApi.Controllers;

public class GatewayController(IGatewayService gatewayService) : BaseController
{
    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<GetAllProductModel>> SendGatewayGetAllProductsRequest()
    {
        return await gatewayService.GetGatewayResponseDataAsync<IEnumerable<GetAllProductModel>>(HttpMethod.Get, PathConstants.GetAllProductsPath, null!,null!, User);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<ProductSearchModel>> SendGatewaySearchProductsByNameRequest(string productName)
    {
        return await gatewayService.GetGatewayResponseDataAsync<IEnumerable<ProductSearchModel>>(HttpMethod.Get, PathConstants.SearchProductsByNamePath, productName, "productName", User);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<ProductSearchModel>> SendGatewaySearchProductsBySubstringRequest(string productNameSubstring)
    {
        return await gatewayService.GetGatewayResponseDataAsync<IEnumerable<ProductSearchModel>>(HttpMethod.Get, PathConstants.SearchProductsBySubstringPath, productNameSubstring, "productNameSubstring", User);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IEnumerable<ProductSortingModel>> SendGatewaySortProductsByFieldRequest(
        SortFieldEnum sortField = SortFieldEnum.Name,
        SortOrderEnum sortOrder = SortOrderEnum.Ascending)
    {
        return await gatewayService.GetGatewayResponseDataAsync<IEnumerable<ProductSortingModel>>(HttpMethod.Get, PathConstants.SortProductsByFieldPath, sortField, sortOrder);
    }

    [HttpPost]
    public async Task SendGatewayProductCreateRequest(ProductCreationModel model)
    {
        await gatewayService.GetGatewayResponseStatusAsync(HttpMethod.Post, PathConstants.ProductCreatePath, model, User);
    }

    [HttpPut]
    public async Task SendGatewayProductEditRequest(ProductModificationModel model)
    {
        await gatewayService.GetGatewayResponseStatusAsync(HttpMethod.Put, PathConstants.ProductEditPath, model, User);
    }

    [HttpDelete]
    public async Task SendGatewayProductDeleteRequest(ProductDeletionModel model)
    {
        await gatewayService.GetGatewayResponseStatusAsync(HttpMethod.Delete, PathConstants.ProductDeletePath, model, User);
    }
}