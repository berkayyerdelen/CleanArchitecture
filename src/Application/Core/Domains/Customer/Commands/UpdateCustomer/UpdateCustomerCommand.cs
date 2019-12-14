using MediatR;

namespace Core.Domains.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommand : IRequest<Unit>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public bool IsActive { get; set; }
        public UpdateCustomerDetailsDto CustomerDetailsDto { get; set; }
    }

    public class UpdateCustomerDetailsDto
    {
        public string CompanyName { get; set; }
        public string ContactName { get; set; }
        public string ContactTitle { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public UpdateCustomerCommand CustomerDto { get; set; }
        public string Fax { get; set; }
    }
}