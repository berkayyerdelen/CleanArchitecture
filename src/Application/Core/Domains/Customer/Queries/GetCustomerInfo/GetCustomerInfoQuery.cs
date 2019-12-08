using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Customer.Queries.GetCustomerInfo
{
    public class GetCustomerInfoQuery : IRequest<string>
    {
        public GetCustomerInfoLookModel CustomerInfoLookModel { get; set; }

        public GetCustomerInfoQuery(GetCustomerInfoLookModel customerInfoLookModel)
        {
            CustomerInfoLookModel = customerInfoLookModel;
        }
        public class Handler : IRequestHandler<GetCustomerInfoQuery, string>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
                => _context = context;
            public Task<string> Handle(GetCustomerInfoQuery request, CancellationToken cancellationToken)
            {
                var customerName = _context.Set<Entities.Customer>().SingleOrDefaultAsync
                    (x => x.Id == request.CustomerInfoLookModel.UserId, cancellationToken).Result;
                return Task.FromResult(customerName.FullName);

            }
        }
    }
}