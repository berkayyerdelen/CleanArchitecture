using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Exceptions;
using Core.Comman.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler:IRequest<UpdateCategoryCommand>
    {
        
        public class Handler:IRequestHandler<UpdateCategoryCommand,Unit>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
                => _context = context;
          
            public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Set<Entities.Category>().SingleOrDefaultAsync(x => x.Id == request.Id,cancellationToken);
                if (entity==null)
                {
                    throw new NotFoundException(nameof(Entities.Category),"Object could not found");
                }
                entity.CategoryName = request.CategoryName ?? entity.CategoryName;
                entity.Description = request.Description ?? entity.Description;
                entity.Picture = request.Pictures ?? entity.Picture;
                await _context.SaveChangesAsync(true,cancellationToken);
                return Unit.Value;
            }
        }
    }
}