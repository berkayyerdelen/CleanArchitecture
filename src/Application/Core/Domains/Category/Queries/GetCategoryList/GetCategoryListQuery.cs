using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Category.Queries.GetCategoryList
{
    public class GetCategoryListQuery:IRequest<CategoryListViewModel>
    {
        public class Handler :IRequestHandler<GetCategoryListQuery,CategoryListViewModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper)
                => (_context, _mapper) = (context, mapper);
         
            public async Task<CategoryListViewModel> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
            {
                return new CategoryListViewModel
                {
                    Categories =await _context.Set<Entities.Category>()
                        .ProjectTo<CategoryLookupModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
                };

            }
        }
    }
}