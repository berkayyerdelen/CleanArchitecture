using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstract;

namespace Entities
{
    public class Customer:BaseEntity<int>
    {
        [Required]
        [StringLength(100, ErrorMessage = "The Fullname length cannot exceed 100 characters. ")]
        [Column(Order = 2)]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        [Column(Order = 3)]
        public string Email { get; set; }
        [Column(Order = 4)]
        public bool IsActive { get; set; }
        [Column(Order = 5)]
        public byte[] PasswordSalt { get; set; }
        [Column(Order = 6)]
        public byte[] PasswordHash { get; set; }
        public virtual CustomerDetails CustomerDetails { get; private set; }
    }
}