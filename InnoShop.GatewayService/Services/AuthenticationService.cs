using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace InnoShop.GatewayService.Services;

public class AuthenticationService(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
    : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
{
    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var claims = new List<Claim>();

        foreach (var header in Request.Headers.Where(h => h.Key.StartsWith("X-User-")))
        {
            var claimType = header.Key["X-User-".Length..];
            claims.Add(new Claim(claimType, header.Value!));
        }

        if (claims.IsNullOrEmpty())
        {
            return Task.FromResult(AuthenticateResult.Fail("Unauthenticated."));
        }

        var identity = new ClaimsIdentity(claims, "GatewayAuth");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "GatewayAuth");

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}