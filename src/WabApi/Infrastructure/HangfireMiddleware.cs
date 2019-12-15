using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Comman.Infrastructure.HangFire;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace WabApi.Infrastructure
{
    public class HangfireMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IMediator _mediator;
        public HangfireMiddleware(RequestDelegate next, IMediator mediator)
            => (_mediator, this.next) = (mediator, next);

        public async Task Invoke(HttpContext context)
        {
            //var fireandForgetJob = new FireAndForgetJob();
            //var delayedJob = new DelayedJob();
            var recurringJob = new RecurringJob(_mediator);
            //var continuationsJob = new ContinuationsJob();
            await next(context);
        }
    }
}
