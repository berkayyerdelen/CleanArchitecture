using AutoMapper;
using Core.Interface.Mapping;
using Entities;

namespace Core.Domains.CustomerOperationClaim.Queries.FindCustomerByMail
{
    public class FindCustomerByMailLookUpModel : IHaveCustomMapping
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public byte[] PasswordSalt { get; set; }
        public byte[] PasswordHash { get; set; }

        public void CreateMappings(Profile configuration) =>
            configuration.CreateMap<Customer, FindCustomerByMailLookUpModel>()
                .ForMember(x => x.Id, v => v.MapFrom(c => c.Id))
                .ForMember(x => x.Email, v => v.MapFrom(c => c.Email))
                .ForMember(x => x.FullName, v => v.MapFrom(c => c.FullName));

    }
}