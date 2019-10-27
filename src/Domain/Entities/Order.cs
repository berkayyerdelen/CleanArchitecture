using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Abstract;

namespace Entities
{
    public class Order:BaseEntity<int>
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetails>();
        }
        [Column(Order = 2)]
        public int CustomerId { get; set; }
        [Column(Order = 3)]
        public DateTime? OrderDate { get; set; }
        [Column(Order = 4)]
        public DateTime? RequiredDate { get; set; }
        [Column(Order = 5)]
        public DateTime? ShippedDate { get; set; }
        [Column(Order = 6)]
        public int? ShipVia { get; set; }
        [Column(Order = 7)]
        public decimal? Freight { get; set; }
        [Column(Order = 8)]
        [StringLength(100, ErrorMessage = "The ShipName  length cannot exceed 100 characters. ")]
        public string ShipName { get; set; }
        [Column(Order = 9)]
        [StringLength(100, ErrorMessage = "The ShipAddress  length cannot exceed 100 characters. ")]
        public string ShipAddress { get; set; }
        [Column(Order = 10)]
        [StringLength(100, ErrorMessage = "The ShipCity  length cannot exceed 100 characters. ")]
        public string ShipCity { get; set; }
        [Column(Order = 11)]
        [StringLength(100, ErrorMessage = "The ShipRegion  length cannot exceed 100 characters. ")]
        public string ShipRegion { get; set; }
        [Column(Order = 12)]
        [StringLength(100, ErrorMessage = "The ShipPostalCode  length cannot exceed 100 characters. ")]
        public string ShipPostalCode { get; set; }
        [Column(Order = 13)]
        [StringLength(100, ErrorMessage = "The ShipCountry  length cannot exceed 100 characters. ")]
        public string ShipCountry { get; set; }
        
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; private set; }
    }
}