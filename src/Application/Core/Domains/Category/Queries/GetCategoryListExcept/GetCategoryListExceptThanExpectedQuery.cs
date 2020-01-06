using System.Collections.Generic;
using System.Linq;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Comman.Interface;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Category.Queries.GetCategoryListExcept
{
    public class GetCategoryListExceptThanExpectedQuery : IRequest<GetCategoryListExceptViewModel>
    {
        public class Handler : IRequestHandler<GetCategoryListExceptThanExpectedQuery, GetCategoryListExceptViewModel>
        {
            private readonly IMapper _mapper;
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context, IMapper mapper)
                => (_context, _mapper) = (context, mapper);

            public async Task<GetCategoryListExceptViewModel> Handle(GetCategoryListExceptThanExpectedQuery request,
                CancellationToken cancellationToken)
            {
                return new GetCategoryListExceptViewModel()
                {
                    CategoryListExceptLookupModels = await _context.Set<Entities.Category>().Except(_context.Set<Entities.Category>()
                        .Where(x => x.CategoryName == "")).
                        ProjectTo<GetCategoryListExceptLookupModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken)
                };
            }
        }

    }
}
