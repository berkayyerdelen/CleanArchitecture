using System.Threading;
using System.Threading.Tasks;
using Core.Interface;
using MediatR;

namespace Core.Domains.Category.Commands.CreateCategory
{
    public class CreateCustomerCommand : IRequest
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public class Handler : IRequestHandler<CreateCustomerCommand, Unit>
        {
            private readonly IMediator _mediator;
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }
            public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                var entity = new Entities.Category
                {
                    CategoryName = request.CategoryName,
                    Description = request.Description
                };
                _context.Set<Entities.Category>().Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }

        }
    }
}