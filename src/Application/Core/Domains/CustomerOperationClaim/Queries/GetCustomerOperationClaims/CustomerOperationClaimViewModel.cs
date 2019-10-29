using System.Collections.Generic;
using Core.Domains.CustomerOperationClaim.Queries.GetCustomerOperationClaims;

namespace Core.Domains.CustomerOperationClaim.Queries.GetCustomerOperationClaims
{
    public class CustomerOperationClaimViewModel
    {
        public List<GetCustomerOperationClaimLookupModel> CustomerOpertaionClaimList { get; set; }
    }
}