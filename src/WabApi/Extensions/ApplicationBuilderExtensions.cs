using Microsoft.AspNetCore.Builder;
using WabApi.Infrastructure;

namespace WabApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplicationBuilder(
            this IApplicationBuilder app)
        {
            app.UseMiddleware<SecurityHeadersMiddleware>();
            app.UseMiddleware<HangfireMiddleware>();
            app.UseMiddleware<SerilogMiddleware>();
        }
    }

}
