using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Core.Interface;
using Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Core.Domains.CustomerOperationClaim.Queries.FindCustomerByMail
{
    public class FindCustomerByMailQuery : IRequest<FindCustomerByMailViewModel>
    {
        public string Email { get; set; }

        public FindCustomerByMailQuery(string email)
        {
            Email = email;
        }
        public class Handler : IRequestHandler<FindCustomerByMailQuery, FindCustomerByMailViewModel>
        {
            private readonly IApplicationDbContext _context;
            private readonly IMapper _mapper;

            public Handler(IApplicationDbContext context, IMapper mapper) 
                => (_context, _mapper) = (context, mapper);
            
            public async Task<FindCustomerByMailViewModel> Handle(FindCustomerByMailQuery request, CancellationToken cancellationToken)
            {
                var entity = await _context.Set<Customer>().ProjectTo<FindCustomerByMailLookUpModel>(_mapper.ConfigurationProvider)
                    .SingleOrDefaultAsync(x => x.Email == request.Email, cancellationToken);
                return new FindCustomerByMailViewModel()
                {
                    Id = entity.Id,
                    Email = entity.Email,
                    FullName = entity.FullName,
                    IsActive = entity.IsActive,
                    PasswordHash = entity.PasswordHash,
                    PasswordSalt = entity.PasswordSalt
                };


            }
        }
    }
}