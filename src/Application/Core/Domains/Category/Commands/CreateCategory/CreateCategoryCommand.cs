using System.Threading;
using System.Threading.Tasks;
using Core.Interface;
using MediatR;

namespace Core.Domains.Category.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public byte[] Picture { get; set; }

        public class Handler : IRequestHandler<CreateCategoryCommand, Unit>
        {
            private readonly IMediator _mediator;
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context, IMediator mediator)
                => (_mediator, _context) = (mediator, context);

            public async Task<Unit> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
            {
                var entity = new Entities.Category
                {
                    CategoryName = request.CategoryName,
                    Description = request.Description,
                    Picture = request.Picture
                };
                _context.Set<Entities.Category>().Add(entity);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }

        }
    }
}