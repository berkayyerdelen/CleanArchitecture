using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstract;

namespace Entities
{
    public class Product:BaseEntity<int>
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }
        [Column(Order = 2)]
        public string ProductName { get; set; }
        [Column(Order = 3)]
        public int? CategoryId { get; set; }
        [Column(Order = 4)]
        public string QuantityPerUnit { get; set; }
        [Column(Order = 5)]
        public decimal? UnitPrice { get; set; }
        [Column(Order = 6)]
        public short? UnitsInStock { get; set; }
        [Column(Order = 7)]
        public short? UnitsOnOrder { get; set; }
        [Column(Order = 8)]
        public short? ReorderLevel { get; set; }
        [Column(Order = 9)]
        public bool Discontinued { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        
        public virtual ICollection<OrderDetails> OrderDetails { get; private set; }
    }
}