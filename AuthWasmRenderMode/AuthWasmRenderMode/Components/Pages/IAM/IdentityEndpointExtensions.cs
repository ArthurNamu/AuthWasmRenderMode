using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AuthWasmRenderMode.Components.Pages.IAM
{
    internal static class IdentityEndpointExtensions
    {
        public static IEndpointConventionBuilder MapIdentityEndpoints(this IEndpointRouteBuilder endpoints)
        {
            ArgumentNullException.ThrowIfNull(endpoints);

            var accountGroup = endpoints.MapGroup("/Account");

            accountGroup.MapPost("/logout", async (
             HttpContext context,
             [FromForm] string returnUrl
             ) =>
            {
                // a more generic implementation would pass the provider as well but we know it's Microsoft here
                string provider = "TdtsCookie";

                var properties = new AuthenticationProperties { RedirectUri = returnUrl };

                // Sign out any existing user
                await context.SignOutAsync("TdtsCookie");

                return TypedResults.Challenge(properties, [provider]);
            });

            return accountGroup;
        }
    }
}
