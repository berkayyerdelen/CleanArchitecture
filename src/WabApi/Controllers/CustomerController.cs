using System.Threading;
using System.Threading.Tasks;
using Core.Domains.Customer.Commands.CreateCustomer;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WabApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("CreateCustomer")]
        public async Task<Unit> CreateCustomer(CreateCustomerCommand request, CancellationToken ct)
            => await _mediator.Send(request, ct);

    }
}