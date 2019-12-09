using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Interface.AppUserSession;
using Core.Domains.Customer.Queries.GetCustomerInfo;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Comman.Behaviours
{
    public class RequestLogger<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
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

        
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = await next();
            var requestName = typeof(TRequest).Name;
            var userId = _appUser.JwtUserIdParse();
            var userName = _mediator.Send(new GetCustomerInfoQuery(new GetCustomerInfoLookModel() { UserId = userId }));
            _logger.LogInformation($"Request performed by {userId}, {userName}, {requestName}");
            return response;

        }
    }
}