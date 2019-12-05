using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Exceptions;
using Core.Comman.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.OperationClaim.Command.DeleteOperationClaim
{
    public class DeleteOperationClaimCommandHandler:IRequest<Unit>
    {
        public string OperationClaimName { get; set; }

        public DeleteOperationClaimCommandHandler(string operationClaimName) => OperationClaimName = operationClaimName;
        public class Handler:IRequestHandler<DeleteOperationClaimCommandHandler,Unit>
        {
            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context) => 
                _context = context;
            public async Task<Unit> Handle(DeleteOperationClaimCommandHandler request, CancellationToken cancellationToken)
            {
                var entity = _context.Set<Entities.OperationClaim>()
                    .SingleOrDefaultAsync(x => x.Name == request.OperationClaimName,cancellationToken).Result;
                
                if(entity is null)
                    throw new NotFoundException(nameof(Entities.OperationClaim), request.OperationClaimName);
                _context.Set<Entities.OperationClaim>().Remove(entity);
                await _context.SaveChangesAsync(true, cancellationToken);

                return Unit.Value;
            }
        }
    }
}