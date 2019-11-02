using FluentValidation;

namespace Core.Domains.OperationClaim.Command.DeleteOperationClaim
{
    public class DeleteOperationClaimCommandHandlerValidator:AbstractValidator<DeleteOperationClaimCommandHandler>
    {
        public DeleteOperationClaimCommandHandlerValidator()
        {
            RuleFor(x => x.OperationClaimName).MaximumLength(100);
        }
    }
}