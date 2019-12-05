using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Interface;
using Core.Comman.Security;
using MediatR;

namespace Core.Domains.Customer.Commands.CreateCustomer
{
    public class CreateCustomerCommandHandler : IRequest<CreateCustomerCommand>
    {
        public class Handler : IRequestHandler<CreateCustomerCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
                => _context = context;

            public async Task<Unit> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);

                var customer = new Entities.Customer()
                {
                    Email = request.Email,
                    FullName = request.FullName,
                    IsActive = true,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };
                await _context.Set<Entities.Customer>().AddAsync(customer, cancellationToken);
                await _context.SaveChangesAsync(true, cancellationToken);
                return Unit.Value;
            }
        }
    }
}