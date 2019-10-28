using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Exceptions;
using Core.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Category.Commands.DeleteCategory
{
    public class DeleteCategoryCommand:IRequest
    {
        public int Id { get; set; }
        public class Handler:IRequestHandler<DeleteCategoryCommand,Unit>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context, IMediator mediator)
            {
                _context = context;
            }
            public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Set<Entities.Category>().FindAsync(request.Id);
                if (entity== null)
                {
                    throw new NotFoundException(nameof(Entities.Category),request.Id);
                }

                _context.Set<Entities.Category>().Remove(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }

         
    }
}