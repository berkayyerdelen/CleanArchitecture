using Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class OrderDetails:BaseEntity<int>
    {
        [Column(Order = 2)]
        public int OrderId { get; set; }
        [Column(Order = 3)]
        public int ProductId { get; set; }
        [Column(Order = 4)]
        public decimal UnitPrice { get; set; }
        [Column(Order = 5)]
        public short Quantity { get; set; }
        [Column(Order = 6)]
        public float Discount { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}