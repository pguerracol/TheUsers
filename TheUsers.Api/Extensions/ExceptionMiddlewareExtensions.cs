using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using TheUsers.Api.CustomMiddlewares;
using TheUsers.Domain.Models;
using TheUsers.Domain.Models.Exceptions;

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

                        var exception = contextFeature.Error;
                        int responseCode = 500;
                        switch (exception)
                        {
                            case UserNotFoundException _:
                                responseCode = (int)HttpStatusCode.NotFound;
                                break;
                            case EmailAlreadyExistsException _:
                                responseCode = (int)HttpStatusCode.BadRequest;
                                break;
                            case UnauthorizedException _:
                                responseCode = (int)HttpStatusCode.Unauthorized;
                                break;
                            case Exception _:
                                responseCode = (int)HttpStatusCode.InternalServerError;
                                break;
                            default:
                                responseCode = (int)HttpStatusCode.InternalServerError;
                                break;
                        }
                        context.Response.StatusCode = responseCode;

                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = responseCode,
                            Message = $"Server exception. {contextFeature.Error.Message}"
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
