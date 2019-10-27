using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;

namespace Thunder.Platform.AspNetCore.UnitOfWork
{
    public class ThunderUnitOfWorkMiddleware
    {
        private readonly RequestDelegate _next;

        public ThunderUnitOfWorkMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IUnitOfWorkManager unitOfWorkManager, IOptions<UnitOfWorkMiddlewareOptions> options)
        {
            if (!options.Value.Filter(httpContext))
            {
                await _next(httpContext);
                return;
            }

            using (var uow = unitOfWorkManager.Begin(options.Value.OptionsFactory(httpContext)))
            {
                await _next(httpContext);
                await uow.CompleteAsync();
            }
        }
    }
}
