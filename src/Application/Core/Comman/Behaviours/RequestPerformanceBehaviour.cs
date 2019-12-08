using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Interface.AppUserSession;
using Core.Domains.Customer.Queries.GetCustomerInfo;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Core.Comman.Behaviours
{
    public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly ILogger<TRequest> _logger;
        private readonly IMediator _mediator;
        private readonly IAppUserIdSession _appUser;

        public RequestPerformanceBehaviour(ILogger<TRequest> logger,
            IMediator mediator, IAppUserIdSession appUser)
        {
            _timer = new Stopwatch();
            _logger = logger;
            _mediator = mediator;
            _appUser = appUser;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();

            var response = await next();

            _timer.Stop();

            var elapsedMilliseconds = _timer.ElapsedMilliseconds;

            if (elapsedMilliseconds > 500)
            {
                var requestName = typeof(TRequest).Name;
                var userId = _appUser.JwtUserIdParse();
                var userName = _mediator.Send(new GetCustomerInfoQuery(new GetCustomerInfoLookModel() { UserId = userId }));

                _logger.LogWarning($" Running Request: {requestName} ({elapsedMilliseconds} milliseconds) {userId} {userName} {request}");
            }

            return response;
        }
    }
}
