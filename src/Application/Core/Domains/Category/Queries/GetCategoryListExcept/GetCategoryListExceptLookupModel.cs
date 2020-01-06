using AutoMapper;
using Core.Comman.Interface.Mapping;
using Core.Domains.Category.Queries.GetCategoryListExistWithProduct;

namespace Core.Domains.Category.Queries.GetCategoryListExcept
{
    public class GetCategoryListExceptLookupModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Entities.Category, GetCategoryListExceptLookupModel>()
                .ForMember(x => x.CategoryName, c => c.MapFrom(v => v.CategoryName))
                .ForMember(x => x.Id, c => c.MapFrom(v => v.Id));

        }
    }
}