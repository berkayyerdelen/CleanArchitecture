using AutoMapper;
using Core.Comman.Interface.Mapping;
using System.Collections.Generic;

namespace Core.Domains.Category.Queries.GetCategoryListExistWithProduct
{
    public class GetCategoryListExistsWithProductLookupModel : IHaveCustomMapping
    {
        public GetCategoryListExistsWithProductLookupModel()
        {
            
        }
        public int Id { get; set; }
        public string CategoryName { get; set; }
        

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Entities.Category, GetCategoryListExistsWithProductLookupModel>()
                .ForMember(x => x.CategoryName, c => c.MapFrom(v => v.CategoryName))
                .ForMember(x => x.Id, c => c.MapFrom(v => v.Id));

        }
    }

  
}