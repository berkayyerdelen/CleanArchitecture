using FluentValidation;

namespace Core.Domains.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator:AbstractValidator<CreateCustomerCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Description).MaximumLength(100).NotEmpty();
            RuleFor(x => x.CategoryName).MaximumLength(100).NotEmpty();
        }
    }
}