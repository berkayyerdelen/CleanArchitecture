using System;
using System.Collections.Generic;
using AutoMapper;
using Core.Comman.Interface.Mapping;
using Entities;
using MediatR;

namespace Core.Domains.Customer.Commands.CreateOrderwithDetails
{
    public class CreateOrderWithDetailsCommand : IRequest,IHaveCustomMapping
    {

        public CreateOrderWithDetailsCommand()
        {
            OrderDetails = new HashSet<Details>();
        }

       
        public int CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipAddress { get; set; }
        public string ShipCity { get; set; }
        public string ShipRegion { get; set; }
        public string ShipPostalCode { get; set; }
        public string ShipCountry { get; set; }
        public ICollection<Details> OrderDetails { get; private set; }


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<CreateOrderWithDetailsCommand, Order>();
            configuration.CreateMap<Details, OrderDetails>();
        }
    }

    public class Details
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public decimal UnitPrice { get; set; }
        public short Quantity { get; set; }
        public float Discount { get; set; }

    }
}