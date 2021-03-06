﻿using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Exceptions;
using Core.Comman.Interface;
using MediatR;

namespace Core.Domains.Category.Commands.DeleteCategory
{
    public class DeleteCategoryCommand:IRequest
    {
        public int Id { get; set; }
        public class Handler:IRequestHandler<DeleteCategoryCommand,Unit>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
                => _context = context;

            public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Set<Entities.Category>().FindAsync(request.Id);
                if (entity is null)
                {
                    throw new NotFoundException(nameof(Entities.Category),request.Id);
                }

                _context.Set<Entities.Category>().Remove(entity);
                await _context.SaveChangesAsync(true, cancellationToken);
                return Unit.Value;
            }
        }

         
    }
}