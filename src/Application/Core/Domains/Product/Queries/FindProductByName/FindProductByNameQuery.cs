using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Comman.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Product.Queries.FindProductByName
{
    public class FindProductByNameQuery:IRequest<FindProductByNameViewModel>
    {
        public string ProductName { get; set; }

        public FindProductByNameQuery(string productname)
        {
            ProductName = productname;
        }

        public class Handler:IRequestHandler<FindProductByNameQuery,FindProductByNameViewModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
                => (_context, _mapper) = (context, mapper);

            public async Task<FindProductByNameViewModel> Handle(FindProductByNameQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.Set<Entities.Product>()
                    .ProjectTo<FindProductByNameLookupModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(x => x.ProductName == request.ProductName, cancellationToken);
                return new FindProductByNameViewModel()
                {
                    Id =entity.Id,
                    CategoryId = entity.CategoryId,
                    Discontinued = entity.Discontinued,
                    ProductName = entity.ProductName,
                    UnitsOnOrder = entity.UnitsOnOrder,
                    QuantityPerUnit = entity.QuantityPerUnit,
                    ReOrderLevel = entity.ReOrderLevel,
                    UnitsInStock = entity.UnitsInStock,
                    UnitPrice = entity.UnitPrice
                };
            }
        }
    }
}