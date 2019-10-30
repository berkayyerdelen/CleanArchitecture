using System.Collections.Generic;
using AutoMapper;
using Core.Interface.Mapping;

namespace Core.Domains.Category.Queries.GetCategoryList
{
    public class CategoryLookupModel:IHaveCustomMapping
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public List<Entities.Product> Product { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Entities.Category, CategoryLookupModel>()
                .ForMember(x => x.CategoryName, c => c.MapFrom(v => v.CategoryName))
                .ForMember(x => x.Id, c => c.MapFrom(v => v.Id))
                .ForMember(x=>x.Product,c=>c.MapFrom(v=>v.Products));

        }
    }
}