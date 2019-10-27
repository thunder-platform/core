using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Thunder.Platform.AspNetCore.Cors
{
    public static class ThunderCorsConfiguration
    {
        public static IServiceCollection AddThunderCors(this IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy(
                "CorsPolicy",
                builder =>
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()));

            return services;
        }

        public static IApplicationBuilder UseThunderCors(this IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
            return app;
        }
    }
}
