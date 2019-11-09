using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Security.Jwt;
using Core.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Customer.Queries.CreateAccessToken
{
    public class CreateAccessTokenQuery : IRequest<CreateAccessTokenViewModel>
    {
        public int CustomerId { get; set; }

        public CreateAccessTokenQuery(int customerId)
            => CustomerId = customerId;

        public class Handler : IRequestHandler<CreateAccessTokenQuery, CreateAccessTokenViewModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ITokenHelper _tokenHelper;

            public Handler(IApplicationDbContext context, ITokenHelper tokenHelper)
                => (_context, _tokenHelper) = (context, tokenHelper);

            public async Task<CreateAccessTokenViewModel> Handle(CreateAccessTokenQuery request, CancellationToken cancellationToken)
            {
                var claims = (from operationClaim in _context.Set<Entities.OperationClaim>()
                    join customerOperationClaim in _context.Set<Entities.CustomerOperationClaim>() on operationClaim.Id
                        equals customerOperationClaim.OperationClaimId
                    where customerOperationClaim.CustomerId == request.CustomerId
                    select new Entities.OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name }).ToList();
                var customer = await _context.Set<Entities.Customer>().SingleOrDefaultAsync(x => x.Id == request.CustomerId, cancellationToken);
                var accestoken = _tokenHelper.CreateToken(customer, claims);
                return new CreateAccessTokenViewModel()
                {
                    Token = accestoken.Token,
                    ExpirationDate = accestoken.Expiration
                };
            }
        }
    }
}