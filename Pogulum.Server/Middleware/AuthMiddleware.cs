using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

public class AuthMiddleware
{
    private readonly List<string> SAFE_PATHS = new List<string> { "/login", "/register", "/error", "/api/auth/login" };

    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    private bool SafePath(string path)
    {
        return SAFE_PATHS.Contains(path) || path.StartsWith("/api");
    }

    public async Task Invoke(HttpContext context)
    {
        var path = context.Request.Path.ToString().ToLower();
        var jwt = context.Request.Cookies["sid"];

        if (SafePath(path))
        {
            await _next(context);
            return;
        }

        if (jwt == null)
        {
            context.Response.Redirect("/login");
            return;
        }

        var handler = new JwtSecurityTokenHandler();
        var jwtSecurityToken = handler.ReadJwtToken(jwt);
        var name = jwtSecurityToken.Claims.First(claim => claim.Type == ClaimTypes.Name).Value;

        await _next(context);
    }
}