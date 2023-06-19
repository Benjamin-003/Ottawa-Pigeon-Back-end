using Microsoft.AspNetCore.Http;
using Ottawa.Pigeon.Application.Exceptions;
using Ottawa.Pigeon.Application.Interfaces;

namespace Ottawa.Pigeon.Infrastructure.Authorization
{
    /// <summary>
    /// Classe qui accède à l'utilisateur rattaché au HttpContext
    /// </summary>
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccessor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Méthode qui identifie l'utilisateur rattaché au HttpContext
        /// </summary>
        /// <returns>un id</returns>
        private int? GetCurrentUserId()
        {
            if (_httpContextAccessor.HttpContext == null || string.IsNullOrEmpty(_httpContextAccessor.HttpContext.Items["UserId"].ToString()))
                return null;
            var httpContextUserId = (int?)_httpContextAccessor.HttpContext.Items["UserId"];
            return httpContextUserId;
        }

        /// <summary>
        /// Méthode qui autorise ou non l'accès à l'utilisateur
        /// </summary>
        /// <param name="requestId">L'id du user dont on veut les données</param>
        /// <returns>true</returns>
        /// <exception cref="UnauthorizedException"></exception>
        public bool AllowUserAccess(int requestId)
        {
            if (GetCurrentUserId() != requestId)
                throw new UnauthorizedException("Access Forbidden.");
            return true;
        }
    }
}
