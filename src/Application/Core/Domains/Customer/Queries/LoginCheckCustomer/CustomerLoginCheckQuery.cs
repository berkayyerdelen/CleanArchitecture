using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Exceptions;
using Core.Comman.Security;
using Core.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Customer.Queries.LoginCheckCustomer
{
    public class CustomerLoginCheckQuery : IRequest<CustomerLoginCheckUpLookModel>
    {
        public class Handler : IRequestHandler<CustomerLoginCheckUpLookModel, bool>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
                => _context = context;

            public async Task<bool> Handle(CustomerLoginCheckUpLookModel request, CancellationToken cancellationToken)
            {
                var customer = _context.Set<Entities.Customer>().SingleOrDefaultAsync(x => x.Email == request.Email,cancellationToken).Result;
                if (customer is null)
                {
                    throw new NotFoundException(nameof(Entities.Customer),request.Password);
                }

                if (!HashingHelper.VerifyPasswordHash(request.Password, customer.PasswordHash, customer.PasswordSalt))
                {
                    throw new VerifyPasswordHashException(nameof(Entities.Customer), "Password Not matched");
                }

                return true;
            }
        }
    }
}