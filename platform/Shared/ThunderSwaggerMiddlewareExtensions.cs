using Swashbuckle.AspNetCore.SwaggerUI;

// ReSharper disable once CheckNamespace
namespace Microsoft.AspNetCore.Builder
{
    public static class ThunderSwaggerMiddlewareExtensions
    {
        public static IApplicationBuilder UseThunderSwagger(this IApplicationBuilder app, string serviceName, string version)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{version}/swagger.json", serviceName);
                c.DocExpansion(DocExpansion.None);
            });

            return app;
        }
    }
}
