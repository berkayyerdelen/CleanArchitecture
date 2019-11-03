using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Domains.OperationClaim.Command.CreateOperationClaim;
using Core.Domains.OperationClaim.Command.DeleteOperationClaim;
using Core.Domains.OperationClaim.Command.UpdateOperationClaim;
using Core.Domains.OperationClaim.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WabApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationClaimController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OperationClaimController(IMediator mediator)
            => _mediator = mediator;


        [HttpGet]
        public async Task<OperationClaimListViewModel> GetOperationClaimList(CancellationToken ct)
            => await _mediator.Send(new GetOperationClaimListQuery(), ct);


        [HttpPost]
        public async Task<Unit> CreateOperationClaim(CreateOperationClaimCommandHandler request, CancellationToken ct)
            => await _mediator.Send(request, ct);


        [HttpDelete]
        public async Task<Unit> DeleteOperationClaim(DeleteOperationClaimCommandHandler request, CancellationToken ct)
            => await _mediator.Send(request, ct);


        [HttpPut]
        public async Task<Unit> UpdateOperationClaim(UpdateOperationClaimCommandHandler request, CancellationToken ct)
            => await _mediator.Send(request, ct);
    }
}