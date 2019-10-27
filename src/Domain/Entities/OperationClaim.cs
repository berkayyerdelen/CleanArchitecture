using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstract;

namespace Entities
{
    public class OperationClaim : BaseEntity<int>
    {
        [Column(Order = 2)]
        [StringLength(100, ErrorMessage = "The Name length cannot exceed 100 characters. ")]
        public string Name { get; set; }
    }
}