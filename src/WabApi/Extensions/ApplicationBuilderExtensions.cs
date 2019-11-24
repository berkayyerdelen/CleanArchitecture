using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Comman.HangFire;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace WabApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplicationBuilder(
            this IApplicationBuilder app)
        {
            app.UseMiddleware<SecurityHeadersMiddleware>();
            app.UseMiddleware<HanfireMiddleware>();
        }
    }
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate next;

        public SecurityHeadersMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add(
                "Content-Security-Policy", "style-src 'self' " +
                                           "https://stackpath.bootstrapcdn.com;" +
                                           "frame-ancestors 'none'");
            context.Response.Headers.Add(
                "Feature-Policy", "camera 'none'");
            context.Response.Headers.Add(
                "X-Content-Type-Options", "nosniff");
            await next(context);
        }

    }

    public class HanfireMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IMediator _mediator;
        public HanfireMiddleware(RequestDelegate next, IMediator mediator)
        {
            _mediator = mediator;
            this.next = next;
        }
      

        public async Task Invoke(HttpContext context)
        {
            //var fireandForgetJob = new FireAndForgetJob();

            //var delayedJob = new DelayedJob();

            var recurringJob= new RecurringJob(_mediator);

            //var continuationsJob = new ContinuationsJob();
            await next(context);
        }

    }
}
