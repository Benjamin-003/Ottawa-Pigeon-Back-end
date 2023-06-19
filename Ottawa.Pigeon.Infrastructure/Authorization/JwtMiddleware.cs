using Microsoft.AspNetCore.Http;

namespace Ottawa.Pigeon.Infrastructure.Authorization
{
    /// <summary>
    /// Custom Middleware qui extrait le token d'une requête et appelle la méthode qui vérifie la validité du token
    /// </summary>
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateToken(token);
            if (userId != null)
            {
                // attach user id to context on successful jwt validation
                context.Items["UserId"] = userId;
            }

            await _next(context);
        }
    }
}
