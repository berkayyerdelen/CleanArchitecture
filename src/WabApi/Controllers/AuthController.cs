using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Domains.Customer.Queries.CreateAccessToken;
using Core.Domains.Customer.Queries.LoginCheckCustomer;
using Core.Interface;
using Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WabApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IApplicationDbContext _context;
        
        public AuthController(IMediator mediator, IApplicationDbContext context)
            => (_mediator, _context) = (mediator, context);

        [HttpPost("Login")]
        [AllowAnonymous]
        public  Task<CreateAccessTokenViewModel> Login(CustomerLoginCheckQuery request,CancellationToken ct)
        {
            var userToLogin = _mediator.Send(request, ct).Result;
            var result = _mediator.Send(new CreateAccessTokenQuery(userToLogin.Id));
            return result;
        }
    }
}