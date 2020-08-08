using Microsoft.AspNetCore.Builder;
using NorthWindApp.Middleware;

namespace NorthWindApp.Helpers
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCacheImageMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CacheImageMiddleware>();
        }
    }
}
