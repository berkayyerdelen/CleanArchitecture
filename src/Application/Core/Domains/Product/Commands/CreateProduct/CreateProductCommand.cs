using System.Threading;
using System.Threading.Tasks;
using Core.Interface;
using MediatR;

namespace Core.Domains.Product.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest
    {
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public short UnitsOnOrder { get; set; }
        public short ReOrderLevel { get; set; }
        public bool Discontinued { get; set; }
        public class Handler : IRequestHandler<CreateProductCommand, Unit>
        {
            public readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
                => _context=context;

            public async Task<Unit> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                
                var entity = new Entities.Product()
                {
                    CategoryId = request.CategoryId,
                    Discontinued = request.Discontinued,
                    ProductName = request.ProductName,
                    QuantityPerUnit = request.QuantityPerUnit,
                    ReorderLevel = request.ReOrderLevel,
                    UnitPrice = request.UnitPrice,
                    UnitsInStock = request.UnitsInStock,
                    UnitsOnOrder = request.UnitsOnOrder

                };
                await _context.Set<Entities.Product>().AddAsync(entity, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }

    }
}