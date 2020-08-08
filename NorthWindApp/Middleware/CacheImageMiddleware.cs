using Microsoft.AspNetCore.Http;
using NorthWindApp.BLL.Interfaces;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace NorthWindApp.Middleware
{
    public class CacheImageMiddleware
    {
        private readonly RequestDelegate _next;

        public CacheImageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ICacheImageService cache)
        {
            Stream originalBody = context.Response.Body;

            //context.Request.EnableBuffering();
            if (context.Request.Path.ToString()
                .ToLower().Contains("image"))
            {
                var key = context.Request.Path.ToString();
                var image = await cache.GetImage(key);
                if (image != null)
                {
                    context.Response.ContentType = "image/png";
                    await context.Response.Body.WriteAsync(image);
                }
            }

            try
            {
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;

                    await _next(context);

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);

                    if (context.Response.StatusCode == (int)HttpStatusCode.OK
                        && context.Response.ContentType == "image/png")
                    {
                        try
                        {
                            memStream.Position = 0;
                            var responseBody = memStream.GetBuffer();

                            await cache.SetImage(context.Request.Path.ToString(), responseBody);
                        }
                        catch (System.Exception ex)
                        {
                            throw new System.Exception("Error writing response form cache", ex);
                        }
                    }
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }


            //await _next(context);


        }
    }
}
