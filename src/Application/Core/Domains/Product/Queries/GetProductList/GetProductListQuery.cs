using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Comman.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Product.Queries.GetProductList
{
    public class GetProductListQuery : IRequest<ProductListViewModel>
    {
        public class Handler : IRequestHandler<GetProductListQuery, ProductListViewModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, IMapper mapper) 
                => (_context, _mapper) = (context, mapper);

            public async Task<ProductListViewModel> Handle(GetProductListQuery request, CancellationToken cancellationToken)
            {
                return new ProductListViewModel
                {
                    Products = await _context.Set<Entities.Product>()
                        .ProjectTo<ProductLookupModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
                };
                
            }
    }
}
}