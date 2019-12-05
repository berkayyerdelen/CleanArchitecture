using AutoMapper;
using Core.Comman.Interface.Mapping;

namespace Core.Domains.Product.Queries.GetProductList
{
    public class ProductLookupModel:IHaveCustomMapping
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal UnitsInStock { get; set; }
        public short UnitsOnOrder { get; set; }
        public short ReOrderLevel { get; set; }
        public bool Discontinued { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Entities.Product, ProductLookupModel>()
                .ForMember(x => x.Id, v => v.MapFrom(c => c.Id));
        }
    }
}