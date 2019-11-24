using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interface;
using Couchbase.Extensions.Caching;
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
            private readonly IDistributedCache _cache;
            public Handler(IApplicationDbContext context, IMapper mapper, IDistributedCache cache)
                => (_context, _mapper, _cache) = (context, mapper, cache);

            public async Task<CategoryListViewModel> Handle(GetCategoryListQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var cachedData = _cache.Get<CategoryListViewModel>("CacheCategories");
                    if (cachedData is null)
                    {
                        var categoryList = new CategoryListViewModel
                        {
                            Categories = await _context.Set<Entities.Category>()
                                .ProjectTo<CategoryLookupModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
                        };
                        _cache.Set("CacheCategories", categoryList, new DistributedCacheEntryOptions()
                        {
                            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                        });
                        return categoryList;
                    }

                    return cachedData;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
              

            }
        }
    }
}