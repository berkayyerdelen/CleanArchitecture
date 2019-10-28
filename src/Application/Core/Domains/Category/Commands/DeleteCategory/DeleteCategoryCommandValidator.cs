using FluentValidation;

namespace Core.Domains.Category.Commands.DeleteCategory
{
    public class DeleteCategoryCommandValidator: AbstractValidator<DeleteCategoryCommand>
    {
        public DeleteCategoryCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();
        }
    }
}