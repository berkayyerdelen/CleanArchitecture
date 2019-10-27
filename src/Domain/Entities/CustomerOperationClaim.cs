using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstract;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Entities
{
    public class CustomerOperationClaim:BaseEntity<int>
    {
        public int CustomerId { get; set; }
        public int OperationClaimId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [ForeignKey("OperationClaimId")]
        public OperationClaim OperationClaim { get; set; }
    }
}