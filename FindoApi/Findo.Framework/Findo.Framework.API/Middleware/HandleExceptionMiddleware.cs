using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Findo.Framework.Interface.Exceptions;


namespace Findo.Framework.API.Middleware
{
    public class HandleExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public HandleExceptionMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (BusinessException e)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.Body = new MemoryStream(Encoding.UTF8.GetBytes(e.Message));
                Serilog.Log.Error(e.Message);
                return;
            }
            catch (Exception e)
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.Body = new MemoryStream(Encoding.UTF8.GetBytes(e.Message));
                Serilog.Log.Error(e.Message);
                return;
            }
        }
    }

    public static class HandleExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseHandleExceptionMiddleware(this IApplicationBuilder builder) => builder.UseMiddleware<HandleExceptionMiddleware>();

    }

}
