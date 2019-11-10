using MediatR;

namespace Core.Domains.Customer.Queries.LoginCheckCustomer
{
    public class CustomerLoginCheckUpLookModel:IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}