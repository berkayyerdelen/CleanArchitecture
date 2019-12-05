using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Comman.Infrastructure;
using Core.Comman.Interface;
using Core.Comman.Interface.Caching;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace Core.Domains.Category.Queries.GetCategoryList
{
    public class GetCategoryListQuery : IRequest<CategoryListViewModel>
    {
        public class Handler : IRequestHandler<GetCategoryListQuery, CategoryListViewModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ICouchBaseRepository<CategoryListViewModel> _couchBaseRepository;

            public Handler(IApplicationDbContext context, IMapper mapper,  ICouchBaseRepository<CategoryListViewModel> couchBaseRepository)
                => (_context, _mapper, _couchBaseRepository) = (context, mapper, couchBaseRepository);

            public Handler(IApplicationDbContext context, IMapper mapper) 
                => (_context, _mapper) = (context, mapper);
            
            public async Task<CategoryListViewModel> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
            {
                var cachedData = _couchBaseRepository.GetByKey("CacheCategories").Result;

                if (cachedData is null)
                {
                    var categoryList = new CategoryListViewModel
                    {
                        Categories = await _context.Set<Entities.Category>()
                            .ProjectTo<CategoryLookupModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
                    };
                    await _couchBaseRepository.Add(categoryList, "CacheCategories");

                    return categoryList;
                }

                return cachedData;

            }
        }
    }
}