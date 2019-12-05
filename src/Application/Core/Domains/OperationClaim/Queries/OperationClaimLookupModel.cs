using AutoMapper;
using Core.Comman.Interface.Mapping;

namespace Core.Domains.OperationClaim.Queries
{
    public class OperationClaimLookupModel:IHaveCustomMapping
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Entities.OperationClaim, OperationClaimLookupModel>()
                .ForMember(x => x.Name, v => v.MapFrom(c => c.Name))
                .ForMember(x => x.Id, v => v.MapFrom(c => c.Id));
        }
    }
}