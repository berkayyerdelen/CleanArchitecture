using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Interface;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.Customer.Queries.CheckCustomerExist
{
    public class CheckCustomerExistQuery:IRequest<bool>
    {
        public string Email { get; set; }

        public CheckCustomerExistQuery(string email)
            => Email = email;

        public class Handler:IRequestHandler<CheckCustomerExistQuery,bool>
        {
            private readonly IApplicationDbContext _context;

            public Handler(IApplicationDbContext context)
                => _context = context;


            public async Task<bool> Handle(CheckCustomerExistQuery request, CancellationToken cancellationToken)
            {
                var customerExist =Task.FromResult(await _context.Set<Entities.Customer>().FirstOrDefaultAsync(x => x.Email == request.Email,cancellationToken)).Result;
                   
                return customerExist != null ? false : true;
            }
        }

    }
}