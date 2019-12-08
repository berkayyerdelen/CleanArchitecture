using MediatR;

namespace Core.Domains.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommand:IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }

    }
}