using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Exceptions;
using Core.Comman.Interface;
using Core.Comman.Security;
using Core.Comman.Security.Jwt;
using Core.Domains.Customer.Queries.CreateAccessToken;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Customer.Queries.LoginCheckCustomer
{
    public class CustomerLoginCheckQuery : IRequest<CreateAccessTokenViewModel>
    {
        public CustomerLoginCheckUpLookModel CustomerLoginCheckUpLookModel { get; set; }
        public class Handler : IRequestHandler<CustomerLoginCheckQuery, CreateAccessTokenViewModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly ITokenHelper _tokenHelper;
            public Handler(IApplicationDbContext context, ITokenHelper tokenHelper)
                => (_context, _tokenHelper) = (context, tokenHelper);

            public async Task<CreateAccessTokenViewModel> Handle(CustomerLoginCheckQuery request, CancellationToken cancellationToken)
            {
                var customer = Task.FromResult(await _context.Set<Entities.Customer>().SingleOrDefaultAsync(x => x.Email == request.CustomerLoginCheckUpLookModel.Email, cancellationToken)).Result;
                if (customer is null)
                {
                    throw new NotFoundException(nameof(Entities.Customer), request.CustomerLoginCheckUpLookModel.Email);
                }

                if (!HashingHelper.VerifyPasswordHash(request.CustomerLoginCheckUpLookModel.Password, customer.PasswordHash, customer.PasswordSalt))
                {
                    throw new VerifyPasswordHashException(nameof(Entities.Customer), "Password Not matched");
                }
                var claims = (from operationClaim in _context.Set<Entities.OperationClaim>()
                              join customerOperationClaim in _context.Set<Entities.CustomerOperationClaim>() on operationClaim.Id
                                  equals customerOperationClaim.OperationClaimId
                              where customerOperationClaim.CustomerId == customer.Id
                              select new Entities.OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name }).ToList();
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