﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InnoShop.GatewayService.WebApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Authorize]
public class BaseController : ControllerBase
{
}