using System;
using System.Threading;
using System.Threading.Tasks;
using Core.Interface;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Domains.Customer.Commands.CreateOrderWithDetails
{
    public class CreateOrderWithDetailsCommandHandler : IRequest
    {
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public int? ShipVia { get; set; }
        public decimal? Freight { get; set; }
        public class Handler : IRequestHandler<CreateOrderWithDetailsCommandHandler, Unit>
        {
            public readonly IApplicationDbContext _context;
            public readonly IHttpContextAccessor _httpContextAccessor;
            public Handler(IApplicationDbContext context)
                => _context = context;
            public Task<Unit> Handle(CreateOrderWithDetailsCommandHandler request, CancellationToken cancellationToken)
            {
                var entity = new Entities.Order()
                {
                    Id = Convert.ToInt32(_httpContextAccessor.HttpContext.Request.Cookies["customerId"]),
                    OrderDate = request.OrderDate,
                    ShippedDate = request.ShippedDate
                };
                return Unit.Task;
            }
        }
    }
}