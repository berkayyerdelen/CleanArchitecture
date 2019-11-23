using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Core.Comman.Extensions;
using Core.Comman.Security.Encryption;
using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Comman.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        public IConfiguration Configuration { get; set; }
        private readonly TokenOptions _tokenOptions;
        private readonly DateTime _accessTokenExpiration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtHelper(IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            Configuration = configuration;
            _tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            _accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOptions.AccessTokenExpiration);
            _httpContextAccessor = httpContextAccessor;
        }
        public AccessToken CreateToken(Customer customer, List<OperationClaim> operationClaims)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(_tokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(_tokenOptions, customer, signingCredentials, operationClaims);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);


            AddJwtToCookie(jwt.EncodedPayload,customer.Id.ToString());
            return new AccessToken()
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(TokenOptions tokenOptions, Customer customer,
            SigningCredentials signingCredentials, List<OperationClaim> operationClaims)
        {
            var jwt = new JwtSecurityToken(
                issuer: tokenOptions.Issuer,
                audience: tokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(customer, operationClaims),
                signingCredentials: signingCredentials
            );
            return jwt;
        }

        private IEnumerable<Claim> SetClaims(Customer customer, List<OperationClaim> operationClaims)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(customer.Id.ToString());
            claims.AddEmail(customer.Email);
            claims.AddName(customer.FullName);
            claims.AddRoles(operationClaims.Select(c => c.Name).ToArray());
            return claims;
        }

        private void AddJwtToCookie(string jwt,string customerId)
        {

            CookieOptions option = new CookieOptions()
            {
                Expires = DateTime.Now.AddMinutes(30)
            };
            
           _httpContextAccessor.HttpContext.Response.Cookies.Append("JWT",jwt, option);
           ISession a;
           

        }
    }
}