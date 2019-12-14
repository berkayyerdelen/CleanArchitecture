using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Extensions;
using Core.Comman.Interface;
using Core.Comman.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Customer.Commands.UpdateCustomer
{
    public class UpdateCustomerHandler : IRequest<UpdateCustomerCommand>
    {
        public class Handler : IRequestHandler<UpdateCustomerCommand, Unit>
        {
            public IApplicationDbContext _context { get; set; }
            public Handler(IApplicationDbContext context)
                => _context = context;
            public async Task<Unit> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
            {
                byte[] passwordHash, passwordSalt;
                HashingHelper.CreatePasswordHash(request.Password, out passwordHash, out passwordSalt);
                var customer = _context.Set<Entities.Customer>().SingleOrDefaultAsync
                    (x => x.PasswordHash == passwordHash && x.Email == request.Email, cancellationToken).Result;

                customer.CustomerDetails.Address = request.CustomerDetailsDto.Address;
                customer.CustomerDetails.City = request.CustomerDetailsDto.City;
                customer.CustomerDetails.CompanyName = request.CustomerDetailsDto.CompanyName;
                customer.CustomerDetails.ContactName = request.CustomerDetailsDto.ContactName;
                customer.CustomerDetails.ContactTitle = request.CustomerDetailsDto.ContactTitle;
                customer.CustomerDetails.Country = request.CustomerDetailsDto.Country;
                customer.CustomerDetails.Fax = request.CustomerDetailsDto.Fax;
                customer.CustomerDetails.Phone = request.CustomerDetailsDto.Phone;
                customer.CustomerDetails.PostalCode = request.CustomerDetailsDto.PostalCode;
                customer.CustomerDetails.Region = request.CustomerDetailsDto.Region;
                customer.FullName = request.FullName;
                customer.IsActive = request.IsActive;
                await _context.SaveChangesAsync(true, cancellationToken);
                return Unit.Value;
            }
        }
    }
}