using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Core.Comman.Exceptions;
using Core.Comman.Interface.AppUserSession;
using Microsoft.AspNetCore.Http;

namespace Core.Comman.Infrastructure.AppUserSessionId
{
    public class AppUserIdSession : IAppUserIdSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppUserIdSession(IHttpContextAccessor httpContextAccessor)
            => (_httpContextAccessor) = (httpContextAccessor);

           
        public int JwtUserIdParse()
        {
            try
            {
                JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var jwt = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
                if (String.IsNullOrEmpty(jwt))
                {
                    throw new CookieIsNullException();
                }

                var readableToken = _jwtSecurityTokenHandler.CanReadToken(jwt);
                var parSecurityToken = _jwtSecurityTokenHandler.ReadToken(jwt) as JwtSecurityToken;
                return Convert.ToInt32(parSecurityToken?.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);

            }
            finally
            {

            }

        }
    }
}