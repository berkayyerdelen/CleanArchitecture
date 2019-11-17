using System.Threading;
using System.Threading.Tasks;
using Core.Interface;
using MediatR;

namespace Core.Domains.OperationClaim.Command.CreateOperationClaim
{
    public class CreateOperationClaimCommandHandler : IRequest
    {
        public string OperationClaimName { get; set; }
        public class Handler : IRequestHandler<CreateOperationClaimCommandHandler, Unit>
        {
            public IApplicationDbContext _context { get; set; }

            public Handler(IApplicationDbContext context) 
                => _context = context;
            public async Task<Unit> Handle(CreateOperationClaimCommandHandler request, CancellationToken cancellationToken)
            {

                 _context.Set<Entities.OperationClaim>().Add(new Entities.OperationClaim()
                {
                    Name = request.OperationClaimName
                });
                 await _context.SaveChangesAsync(true, cancellationToken);
                return Unit.Value;

            }
        }
    }
}