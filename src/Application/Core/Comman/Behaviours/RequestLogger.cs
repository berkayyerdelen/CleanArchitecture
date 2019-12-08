using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Interface.AppUserSession;
using Core.Domains.Customer.Queries.GetCustomerInfo;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Comman.Behaviours
{
    public class RequestLogger<TRequest> : IRequest<TRequest>
    {
        private readonly ILogger _logger;
        private readonly IMediator _mediator;
        private readonly IAppUserIdSession _appUser;

        public RequestLogger(ILogger<TRequest> logger, IMediator mediator, IAppUserIdSession appUser)
        {
            _mediator = mediator;
            _appUser = appUser;
            _logger = logger;
        }

        public void Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _appUser.JwtUserIdParse();
            var userName = _mediator.Send(new GetCustomerInfoQuery(new GetCustomerInfoLookModel(){UserId = userId}));
            _logger.LogInformation($"Request performed by {userId}, {userName}, {requestName}");
           
        }
    }
}