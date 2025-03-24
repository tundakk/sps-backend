using sps.BLL.Services.Interfaces;
using System.Security.Claims;

namespace sps.API.Middleware
{
    /// <summary>
    /// Middleware that extracts JWT tokens from various sources and attaches the user to the HTTP context
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="JwtMiddleware"/> class.
        /// </summary>
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Process the request and extract JWT token from various sources
        /// </summary>
        /// <param name="context">The HTTP context</param>
        public async Task InvokeAsync(HttpContext context)
        {
            // Check if user is already authenticated through normal means
            if (context.User.Identity != null && !context.User.Identity.IsAuthenticated)
            {
                // Try to extract token from multiple possible sources
                var token = ExtractTokenFromRequest(context.Request);

                if (!string.IsNullOrEmpty(token))
                {
                    // Create a scope to resolve the JwtTokenService
                    using var scope = context.RequestServices.CreateScope();
                    var jwtTokenService = scope.ServiceProvider.GetRequiredService<IJwtTokenService>();

                    if (jwtTokenService.ValidateToken(token))
                    {
                        // Token is valid, attach the claims to the current request
                        var claims = jwtTokenService.GetClaimsFromToken(token);
                        AttachUserToContext(context, claims);
                    }
                }
            }

            await _next(context);
        }

        /// <summary>
        /// Extracts JWT token from various sources in the request
        /// </summary>
        private string? ExtractTokenFromRequest(HttpRequest request)
        {
            // 1. Try to get from Authorization header
            var authHeader = request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            {
                return authHeader.Substring("Bearer ".Length).Trim();
            }

            // 2. Try to get from cookies - NextAuth commonly stores token in cookies
            if (request.Cookies.TryGetValue("token", out string? tokenFromCookie) && !string.IsNullOrEmpty(tokenFromCookie))
            {
                return tokenFromCookie;
            }

            // 3. Try to get from NextAuth's session token cookie
            if (request.Cookies.TryGetValue("next-auth.session-token", out string? nextAuthToken) && !string.IsNullOrEmpty(nextAuthToken))
            {
                return nextAuthToken;
            }

            // 4. Try to get from query string - sometimes used in redirects
            if (request.Query.TryGetValue("token", out var tokenFromQuery))
            {
                return tokenFromQuery.ToString();
            }

            return null;
        }

        /// <summary>
        /// Attaches user claims to the current HTTP context
        /// </summary>
        private void AttachUserToContext(HttpContext context, IEnumerable<Claim> claims)
        {
            // Create a ClaimsIdentity and attach it to the current context
            var identity = new ClaimsIdentity(claims, "JWT");
            context.User = new ClaimsPrincipal(identity);
        }
    }
}