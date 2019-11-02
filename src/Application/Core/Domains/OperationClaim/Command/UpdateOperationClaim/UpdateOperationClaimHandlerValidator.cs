using FluentValidation;

namespace Core.Domains.OperationClaim.Command.UpdateOperationClaim
{
    public class UpdateOperationClaimHandlerValidator:AbstractValidator<UpdateOperationClaimCommandHandler>
    {
        public UpdateOperationClaimHandlerValidator()
        {
            RuleFor(x => x.OperationClaimName).NotNull().NotNull().MaximumLength(100);
            RuleFor(x => x.OperationCliamId).NotNull().NotEmpty().NotEqual(0);
        }
    }
}