using Microsoft.IdentityModel.Tokens;

namespace Core.Comman.Security.Encryption
{
    public class SigningCredentialsHelper
    {
        public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
            => new SigningCredentials(securityKey, SecurityAlgorithms./*HmacSha256Signature*/HmacSha256);
       
    }
}