using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Comman.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.OperationClaim.Queries
{
    public class GetOperationClaimListQuery : IRequest<OperationClaimListViewModel>
    {
        public class Handler : IRequestHandler<GetOperationClaimListQuery, OperationClaimListViewModel>
        {
            private IApplicationDbContext _context;
            private IMapper _mapper;
            public Handler(IApplicationDbContext context, IMapper mapper) => (_context, _mapper) = (context, mapper);
            public async Task<OperationClaimListViewModel> Handle(GetOperationClaimListQuery request, CancellationToken cancellationToken)
            {
                return new OperationClaimListViewModel()
                {
                    OperationClaims = await _context.Set<Entities.OperationClaim>()
                        .ProjectTo<OperationClaimLookupModel>(_mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken)

                };

            }
        }
    }
}