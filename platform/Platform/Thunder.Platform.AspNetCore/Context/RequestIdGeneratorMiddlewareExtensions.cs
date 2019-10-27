using Thunder.Platform.AspNetCore.Context;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class RequestIdGeneratorMiddlewareExtensions
    {
        /// <summary>
        /// Apply thunder unit of work middleware. This method should be called before UseMvc.
        /// </summary>
        /// <param name="app">The application builder instance.</param>
        /// <returns>The IApplicationBuilder.</returns>
        public static IApplicationBuilder UserThunderRequestIdGenerator(this IApplicationBuilder app)
        {
            return app.UseMiddleware<RequestIdGeneratorMiddleware>();
        }
    }
}
