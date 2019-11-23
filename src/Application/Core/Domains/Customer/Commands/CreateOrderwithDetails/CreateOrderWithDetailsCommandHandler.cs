using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Comman.Interface.AppUserSession;
using Core.Interface;
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

                var order = new Order()
                {
                    CustomerId = _appUserIdSession.JwtUserIdParse(),
                    Freight = request.Freight,
                    OrderDate = request.OrderDate,
                    RequiredDate = request.RequiredDate,
                    ShipAddress = request.ShipAddress,
                    ShipCity = request.ShipCity,
                    ShipCountry = request.ShipCountry,
                    ShipName = request.ShipName,
                    ShipPostalCode = request.ShipPostalCode,
                    ShipRegion = request.ShipRegion,
                    ShipVia = request.ShipVia,
                    ShippedDate = request.ShippedDate

                };

                await _context.Set<Order>().AddAsync(order, cancellationToken);
                await _context.SaveChangesAsync(true, cancellationToken);
                return Unit.Value;
            }
        }
    }
}