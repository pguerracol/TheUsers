using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using TheUsers.Domain.Models;
using TheUsers.Api.CustomMiddlewares;

namespace TheUsers.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = $"Internal Server error. Something went wrong: {contextFeature.Error.Message}"
                        }.ToString());
                    }
                });
            });
        }
        public static void ConfigureCustomExceptionMiddleware(this WebApplication app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
