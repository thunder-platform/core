using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Thunder.Platform.Core;
using Thunder.Platform.Core.Context;

namespace Thunder.Platform.AspNetCore.Context
{
    internal class RequestIdGeneratorMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestIdGeneratorMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public Task Invoke(HttpContext context, IUserContext userContext)
        {
            string requestId = context.Request.Headers[CommonHttpHeaderNames.RequestId];
            if (string.IsNullOrEmpty(requestId))
            {
                requestId = Guid.NewGuid().ToString("N");
            }

            context.TraceIdentifier = requestId;

            userContext.SetValue(requestId, CommonUserContextKeys.RequestId);

            // apply the request ID to the response header for client side tracking
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add(CommonHttpHeaderNames.RequestId, new[] { context.TraceIdentifier });
                return Task.CompletedTask;
            });

            return _next(context);
        }
    }
}
