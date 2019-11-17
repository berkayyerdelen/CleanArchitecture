using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Exceptions;
using Core.Interface;
using MediatR;

namespace Core.Domains.Product.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { get; set; }


        public class Handler : IRequestHandler<DeleteProductCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
                => _context = context;


            public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Set<Entities.Product>().FindAsync(request.Id);
                if (entity == null)
                {
                    throw new NotFoundException(nameof(Entities.Product), "We couldnt find product that u're looking for");
                }

                _context.Set<Entities.Product>().Remove(entity);
                await _context.SaveChangesAsync(true, cancellationToken);
                return Unit.Value;

            }
        }
    }
}