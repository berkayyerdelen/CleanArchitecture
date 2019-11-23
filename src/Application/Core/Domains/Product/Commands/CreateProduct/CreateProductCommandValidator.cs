using FluentValidation;

namespace Core.Domains.Product.Commands.CreateProduct
{
    public class CreateProductCommandValidator:AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.CategoryId).NotEmpty().NotNull();
            RuleFor(x => x.Discontinued).NotNull();
            RuleFor(x => x.ProductName).MaximumLength(100).NotNull();
        }
    }
}