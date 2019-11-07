using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Exceptions;
using Core.Interface;
using MediatR;

namespace Core.Domains.Product.Commands.UpdateProduct
{
    public class UpdateProductCommand:IRequest
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
        public short UnitsInStock { get; set; }
        public short UnitsOnOrder { get; set; }
        public short ReOrderLevel { get; set; }
        public bool Discontinued { get; set; }

        public class Handler:IRequestHandler<UpdateProductCommand,Unit>
        {
            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
                => _context = context;

            public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Set<Entities.Product>().FindAsync(request.Id);
                if (entity is null)
                {
                    throw new NotFoundException(nameof(Entities.Product), "We couldnt product that u're looking for atm");
                }

                entity.CategoryId = request.Id;
                entity.Discontinued = request.Discontinued;
                entity.ProductName = request.ProductName ?? entity.ProductName;
                entity.QuantityPerUnit = request.QuantityPerUnit ?? entity.QuantityPerUnit;
                entity.UnitPrice = request.UnitPrice == 0 ? entity.UnitPrice : request.UnitPrice;
                entity.ReorderLevel = request.ReOrderLevel == 0 ? entity.ReorderLevel : request.ReOrderLevel;
                entity.UnitsInStock = request.UnitsInStock == 0 ? entity.UnitsInStock : request.UnitsInStock;
                entity.UnitsOnOrder = request.UnitsOnOrder == 0 ? entity.UnitsOnOrder : request.UnitsOnOrder;
                await _context.SaveChangesAsync(cancellationToken);
                return Unit.Value;
                
            }
        }
    }
}