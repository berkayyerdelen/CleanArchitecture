using AutoMapper;
using Core.Comman.Interface.Mapping;

namespace Core.Domains.CustomerOperationClaim.Queries.GetCustomerOperationClaims
{
    public class GetCustomerOperationClaimLookupModel:IHaveCustomMapping
    {
        public int Id { get; set; }
        public string OperationName { get; set; }


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Entities.OperationClaim, GetCustomerOperationClaimLookupModel>()
                .ForMember(x => x.Id, v => v.MapFrom(c => c.Id));

        }
    }

   
}