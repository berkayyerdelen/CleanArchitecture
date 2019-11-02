using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interface;
using Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.CustomerOperationClaim.Queries.GetCustomerOperationClaims
{
    public class GetCustomerOperationClaimListQuery : IRequest<CustomerOperationClaimViewModel>
    {
        public Customer Customer { get; set; }
        public class Handler : IRequestHandler<GetCustomerOperationClaimListQuery, CustomerOperationClaimViewModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, IMapper mapper)
                => (_context, _mapper) = (context, mapper);

            public async Task<CustomerOperationClaimViewModel> Handle(GetCustomerOperationClaimListQuery request, CancellationToken cancellationToken)
            {
              
                var result = from operationclaim in _context.Set<Entities.OperationClaim>()
                             join customerOperationClaim in _context.Set<Entities.CustomerOperationClaim>()
                                 on operationclaim.Id equals customerOperationClaim.OperationClaimId
                             where customerOperationClaim.CustomerId == request.Customer.Id
                             select new Entities.OperationClaim { Id = operationclaim.Id, Name = operationclaim.Name };
                var model = await result.ProjectTo<GetCustomerOperationClaimLookupModel>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);
               
                return new CustomerOperationClaimViewModel
                {
                    CustomerOpertaionClaimList = model
                };
            }
        }
    }
}