using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Comman.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Category.Queries.GetCategoryListExistWithProduct
{
    public class GetCategoryListExistsWithProductQuery : IRequest<GetCategoryListExistsWithProductViewModel>
    {
        public class Handler : IRequestHandler<GetCategoryListExistsWithProductQuery, GetCategoryListExistsWithProductViewModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, IMapper mapper)
                => (_context, _mapper) = (context, mapper);

            public async Task<GetCategoryListExistsWithProductViewModel> Handle(GetCategoryListExistsWithProductQuery request, CancellationToken cancellationToken)
            {
                return  new GetCategoryListExistsWithProductViewModel()
                    {
                        CategoryListExistsWithProduct = await _context.Set<Entities.Category>().
                            Where(x => x.Products.Any(p => p.CategoryId == x.Id)).
                            ProjectTo<GetCategoryListExistsWithProductLookupModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
                    };
                   
                }
        }
    }
}