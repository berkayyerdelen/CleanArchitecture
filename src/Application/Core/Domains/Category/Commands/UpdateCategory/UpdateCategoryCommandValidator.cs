using FluentValidation;

namespace Core.Domains.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator:AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(x => x.CategoryName).Length(100);
            RuleFor(x => x.Description).Length(100);
            RuleFor(x => x.Id).NotNull().NotEmpty();
        }
    }
}