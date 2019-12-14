using FluentValidation;

namespace Core.Domains.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerCommandValidator: AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty();
            RuleFor(x => x.CustomerDetailsDto).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty();
            RuleFor(x => x.FullName).NotNull().NotEmpty();
        }
    }
}