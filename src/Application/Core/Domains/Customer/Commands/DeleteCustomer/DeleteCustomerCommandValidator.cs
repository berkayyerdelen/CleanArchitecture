using FluentValidation;

namespace Core.Domains.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandValidator: AbstractValidator<DeleteCustomerCommandHandler>
    {
        public DeleteCustomerCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}