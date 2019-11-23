using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Core.Comman.Interface.AppUserSession;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Comman.Infrastructure
{
    public class AppUserIdSession : IAppUserIdSession
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppUserIdSession(IHttpContextAccessor httpContextAccessor)
            => (_httpContextAccessor) = (httpContextAccessor);


        public int JwtUserIdParse()
        {
            //try
            //{
            //    JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            //    var jwt = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];
            //    var readableToken = _jwtSecurityTokenHandler.CanReadToken(jwt);
            //    var parSecurityToken = _jwtSecurityTokenHandler.ReadToken(jwt) as JwtSecurityToken;
            //    var a= Convert.ToInt32(parSecurityToken?.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
            //    return a;
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e);
            //    throw;
            //}
            var jwt = _httpContextAccessor.HttpContext.Request.Cookies["jwt"];

            var jwtHandler = new JwtSecurityTokenHandler();
            var jwtInput = jwt;


            //Check if readable token (string is in a JWT format)
            var readableToken = jwtHandler.CanReadToken(jwtInput);

            if (readableToken == true)
            {
                var token = jwtHandler.ReadJwtToken(jwtInput);

                //Extract the headers of the JWT
                var headers = token.Header;
                var jwtHeader = "{";
                foreach (var h in headers)
                {
                    jwtHeader += '"' + h.Key + "\":\"" + h.Value + "\",";
                }

                jwtHeader += "}";
                var k = "Header:\r\n" + JToken.Parse(jwtHeader).ToString(Formatting.Indented);

               

            }
            return 1;
        }
    }
}