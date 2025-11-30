using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly IHttpContextAccessor accessor;

    public JwtAuthenticationStateProvider(IHttpContextAccessor accessor)
    {
        this.accessor = accessor;
    }

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var ctx = accessor.HttpContext;

        if (ctx != null && ctx.User.Identity?.IsAuthenticated == true)
        {
            return Task.FromResult(new AuthenticationState(ctx.User));
        }

        return Task.FromResult(new AuthenticationState(
            new ClaimsPrincipal(new ClaimsIdentity())
        ));
    }

    public void NotifyUserLogout()
    {
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
