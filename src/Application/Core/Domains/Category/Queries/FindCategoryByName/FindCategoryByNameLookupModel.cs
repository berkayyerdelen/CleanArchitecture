using AutoMapper;
using Core.Interface.Mapping;

namespace Core.Domains.Category.Queries.FindCategoryByName
{
    public class FindCategoryByNameLookupModel : IHaveCustomMapping
    {
        public string CategoryName { get; set; }
        public string Descriptipn { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Entities.Category, FindCategoryByNameLookupModel>()
                .ForMember(x => x.CategoryName, c => c.MapFrom(v => v.CategoryName))
                .ForMember(x=>x.Descriptipn, c=>c.MapFrom(v=>v.Description));
        }
    }
}