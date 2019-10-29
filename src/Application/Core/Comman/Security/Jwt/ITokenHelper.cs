using System.Collections.Generic;
using Entities;

namespace Core.Comman.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(Customer user, List<OperationClaim> operationClaims);
    }
}