using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Comman.Interface;
using Core.Comman.Interface.AppUserSession;
using Entities;
using MediatR;

namespace Core.Domains.Customer.Commands.CreateOrderwithDetails
{
    public class CreateOrderWithDetailsCommandHandler : IRequest<CreateOrderWithDetailsCommand>
    {
        public class Handler : IRequestHandler<CreateOrderWithDetailsCommand, Unit>
        {
            private readonly IApplicationDbContext _context;
            private readonly IAppUserIdSession _appUserIdSession;
            private readonly IMapper _mapper;
            public Handler(IApplicationDbContext context, IAppUserIdSession appUserIdSession, IMapper mapper)
                => (_context, _appUserIdSession, _mapper) = (context, appUserIdSession, mapper);


            public async Task<Unit> Handle(CreateOrderWithDetailsCommand request, CancellationToken cancellationToken)
            {

                Order order = _mapper.Map<CreateOrderWithDetailsCommand, Order>(request);
                order.CustomerId = _appUserIdSession.JwtUserIdParse();
                await _context.Set<Order>().AddAsync(order, cancellationToken);
                await _context.SaveChangesAsync(true, cancellationToken);
                return Unit.Value;
            }
        }
    }
}