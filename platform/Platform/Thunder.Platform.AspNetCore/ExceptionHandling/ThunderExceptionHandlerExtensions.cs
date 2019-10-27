using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Thunder.Platform.AspNetCore.ExceptionHandling;
using Thunder.Platform.Core.Exceptions;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class ThunderExceptionHandlerExtensions
    {
        public static IApplicationBuilder UseThunderExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var exceptionHandlerFeature = context.Features.Get<IExceptionHandlerFeature>();

                    // Use exceptionHandlerPathFeature to process the exception (for example,
                    // logging), but do NOT expose sensitive error information directly to
                    // the client.
                    if (exceptionHandlerFeature != null)
                    {
                        var response = new ErrorResponse(exceptionHandlerFeature.Error.ToDisplayError(), HttpStatusCode.InternalServerError);
                        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
                    }
                    else
                    {
                        await context.Response.WriteAsync(JsonSerializer.Serialize(new ErrorResponse("There is an internal server error.")));
                    }
                });
            });

            return app;
        }
    }
}
