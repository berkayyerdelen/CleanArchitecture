using System.Collections.Generic;

namespace Core.Domains.OperationClaim.Queries
{
    public class OperationClaimListViewModel
    {
        public IList<OperationClaimLookupModel> OperationClaims { get; set; }
    }
}