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
    public class CustomerLoginCheckQuery : IRequest<Entities.Customer>
    {
        public CustomerLoginCheckUpLookModel CustomerLoginCheckUpLookModel { get; set; }
        public class Handler : IRequestHandler<CustomerLoginCheckQuery, Entities.Customer>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
                => _context = context;

            public async Task<Entities.Customer> Handle(CustomerLoginCheckQuery request, CancellationToken cancellationToken)
            {
                var customer = Task.FromResult(await _context.Set<Entities.Customer>().SingleOrDefaultAsync(x => x.Email == request.CustomerLoginCheckUpLookModel.Email,cancellationToken)).Result;
                if (customer is null)
                {
                    throw new NotFoundException(nameof(Entities.Customer),request.CustomerLoginCheckUpLookModel.Email);
                }

                if (!HashingHelper.VerifyPasswordHash(request.CustomerLoginCheckUpLookModel.Password, customer.PasswordHash, customer.PasswordSalt))
                {
                    throw new VerifyPasswordHashException(nameof(Entities.Customer), "Password Not matched");
                }

                return customer;
            }
        }
    }
}