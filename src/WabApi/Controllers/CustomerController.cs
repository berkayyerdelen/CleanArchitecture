﻿using System.Threading;
using System.Threading.Tasks;
using Core.Domains.Customer.Commands.CreateCustomer;
using Core.Domains.Customer.Commands.CreateOrderwithDetails;
using Core.Domains.Customer.Commands.DeleteCustomer;
using Core.Domains.Customer.Commands.UpdateCustomer;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WabApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
            => _mediator = mediator;
        
        [HttpPost("CreateCustomer")]
        [Authorize(Roles = "Admin")]
        public async Task<Unit> CreateCustomer(CreateCustomerCommand request, CancellationToken ct)
            => await _mediator.Send(request, ct);

        [HttpPost("CreateOrder")]
        [Authorize(Roles = "Admin")]
        public async Task<Unit> CreateCustomer(CreateOrderWithDetailsCommand request, CancellationToken ct)
            => await _mediator.Send(request, ct);

        [HttpDelete("DeleteCustomer")]
        [Authorize(Roles = "Admin")]
        public async Task<Unit> RemoveCustomer(DeleteCustomerCommandHandler request, CancellationToken ct)
            => await _mediator.Send(request, ct);

        [HttpPut("UpdateCustomer")]
        [Authorize(Roles = "Admin")]
        public async Task<Unit> UpdateCustomer(UpdateCustomerCommand request, CancellationToken ct)
            => await _mediator.Send(request, ct);

    }
}