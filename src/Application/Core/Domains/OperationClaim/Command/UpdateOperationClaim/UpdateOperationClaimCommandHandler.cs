﻿using System.Threading;
using System.Threading.Tasks;
using Core.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.OperationClaim.Command.UpdateOperationClaim
{
    public class UpdateOperationClaimCommandHandler : IRequest
    {
        public int OperationCliamId { get; set; }
        public string OperationClaimName { get; set; }
        public class Handler : IRequestHandler<UpdateOperationClaimCommandHandler, Unit>
        {
            private IApplicationDbContext _context;
            public Handler(IApplicationDbContext context) => 
                _context = context;
            public async Task<Unit> Handle(UpdateOperationClaimCommandHandler request, CancellationToken cancellationToken)
            {
                var entity = _context.Set<Entities.OperationClaim>()
                    .FindAsync(request.OperationCliamId).Result;
                entity.Name = request.OperationClaimName;
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }
    }
}