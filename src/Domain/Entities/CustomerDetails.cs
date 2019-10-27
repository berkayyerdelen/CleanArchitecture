using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using Entities.Abstract;

namespace Entities
{
    public class CustomerDetails:BaseEntity<int>
    {
       
        [Column(Order = 2)]
        public int CustomerId { get; set; }
       
        [Column(Order = 3)]
        [StringLength(100, ErrorMessage = "The CompanyName  length cannot exceed 100 characters. ")]
        public string CompanyName { get; set; }
       
        [Column(Order = 4)]
        [StringLength(100, ErrorMessage = "The ContactName  length cannot exceed 100 characters. ")]
        public string ContactName { get; set; }
      
        [Column(Order = 5)]
        [StringLength(100, ErrorMessage = "The ContactTitle  length cannot exceed 100 characters. ")]
        public string ContactTitle { get; set; }
      
        [Column(Order = 6)]
        [StringLength(100, ErrorMessage = "The Address  length cannot exceed 100 characters. ")]
        public string Address { get; set; }
       
        [Column(Order = 7)]
        [StringLength(100, ErrorMessage = "The City Name length cannot exceed 100 characters. ")]
        public string City { get; set; }
       
        [Column(Order = 8)]
        [StringLength(100, ErrorMessage = "The Region Name length cannot exceed 100 characters. ")]
        public string Region { get; set; }
       
        [Column(Order = 9)]
        [StringLength(100, ErrorMessage = "The PostalCode  length cannot exceed 100 characters. ")]
        public string PostalCode { get; set; }
       
        [Column(Order = 10)]
        [StringLength(100, ErrorMessage = "The Country name length cannot exceed 100 characters. ")]
        public string Country { get; set; }
        
        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [Column(Order = 11)]
        public string Phone { get; set; }
       
        [Column(Order = 12)]
        [StringLength(100, ErrorMessage = "The Fax  length cannot exceed 100 characters. ")]
        public string Fax { get; set; }
       
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

    }
}