﻿
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Core.Comman.Interface.AppUserSession;
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
                //TODO:Parse with cookie parser
                var userId = _appUser.JwtUserIdParse();
                if (userId != 0)
                {
                    //var userName = _mediator.Send(new GetCustomerInfoQuery(new GetCustomerInfoLookModel() { UserId = userId }));
                    _logger.LogWarning($" Running Request: {requestName} ({elapsedMilliseconds} milliseconds) {userId} {request}");
                }
                _logger.LogWarning($" Running Request: {requestName} ({elapsedMilliseconds} milliseconds)   {request}");

            }

            return response;
        }
    }
}
