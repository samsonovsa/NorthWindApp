using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace NorthWindApp.Middleware
{
    public sealed class BodyRewindMiddleware
    {
        private readonly RequestDelegate _next;

        public BodyRewindMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try { 
                context.Request.EnableBuffering(); 
            } 
            catch(Exception ex) 
            {
                throw new Exception("Error enabling request buffering", ex);
            }
            await _next(context);
        }
    }
    public static class BodyRewindExtensions
    {
        public static IApplicationBuilder EnableRequestBodyBuffering(this IApplicationBuilder app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            return app.UseMiddleware<BodyRewindMiddleware>();
        }

    }
}
