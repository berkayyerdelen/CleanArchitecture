using FluentValidation;

namespace Core.Domains.OperationClaim.Command.CreateOperationClaim
{
    public class CreateOperationClaimCommandHandlerValidator:AbstractValidator<CreateOperationClaimCommandHandler>
    {
        public CreateOperationClaimCommandHandlerValidator()
        {
            RuleFor(x => x.OperationClaimName).NotEmpty();
        }
    }
}