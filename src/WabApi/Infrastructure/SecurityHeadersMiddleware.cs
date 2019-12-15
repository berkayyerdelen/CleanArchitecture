using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WabApi.Infrastructure
{
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate next;
        public SecurityHeadersMiddleware(RequestDelegate next)
            => this.next = next;
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
}
