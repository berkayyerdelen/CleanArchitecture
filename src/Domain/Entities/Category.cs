using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstract;

namespace Entities
{
    public class Category:BaseEntity<int>
    {
        public Category()
        {
            Products = new HashSet<Product>();
        }
        [StringLength(100, ErrorMessage = "The Category Name length cannot exceed 100 characters. ")]
        [Column(Order = 2)]
        public string CategoryName { get; set; }
        [StringLength(500, ErrorMessage = "The Description length cannot exceed 100 characters. ")]
        [Column(Order = 3)]
        public string Description { get; set; }
        [Column(Order = 4)]
        public byte[] Picture { get; set; }
        public ICollection<Product> Products { get; private set; }
    }
}