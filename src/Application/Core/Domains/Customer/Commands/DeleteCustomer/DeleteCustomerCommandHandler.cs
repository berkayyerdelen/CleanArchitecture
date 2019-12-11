using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Interface;
using Core.Comman.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Customer.Commands.DeleteCustomer
{
    public class DeleteCustomerCommandHandler : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public DeleteCustomerCommandHandler(string email, string password)
            => (Email, Password) = (email, password);

        public class Handler : IRequestHandler<DeleteCustomerCommandHandler>
        {
            public IApplicationDbContext _context { get; set; }

            public Handler(IApplicationDbContext context)
                => _context = context;
            public async Task<Unit> Handle(DeleteCustomerCommandHandler request, CancellationToken cancellationToken)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                var getUser = _context.Set<Entities.Customer>().SingleOrDefaultAsync
                    (x => x.PasswordHash == passwordHash && x.Email == request.Email, cancellationToken).Result;
                _context.Set<Entities.Customer>().Remove(getUser);
                await _context.SaveChangesAsync(true, cancellationToken);
                return Unit.Value;
            }
        }
    }
}