using AutoMapper;
using Core.Comman.Interface.Mapping;

namespace Core.Domains.Product.Queries.FindProductByName
{
    public class FindProductByNameLookupModel:IHaveCustomMapping
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
            configuration.CreateMap<Entities.Product,FindProductByNameLookupModel>()
                .ForMember(x=>x.Id, c=>c.MapFrom(v=>v.Id)).
                ForMember(x=>x.CategoryId ,c=>c.MapFrom(v=>v.CategoryId)).
                ForMember(x=>x.Discontinued,c=>c.MapFrom(v=>v.Discontinued)).
                ForMember(x=>x.ProductName,c=>c.MapFrom(v=>v.ProductName));
        }
    }
}