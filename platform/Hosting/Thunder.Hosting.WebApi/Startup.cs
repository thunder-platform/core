using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.AspNetCore;
using Thunder.Platform.AspNetCore.Cors;
using Thunder.Service.Authentication;

namespace Thunder.Hosting.WebApi
{
    public class Startup : IThunderStartup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public string ApiName { get; } = "Thunder Test API";

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc(options =>
                {
                    options.Filters.Add<SimpleAuthorizationFilter>();
                })
                .AddControllersAsServices()
                .AddThunderJsonOptions()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

            services.AddThunderCors();

            services.AddThunderSwagger(ApiName, "v1");

            services.Configure<MvcOptions>(mvcOptions =>
            {
                mvcOptions.AddThunderMvcOptions();
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseThunderExceptionHandler();
            app.UserThunderRequestIdGenerator();
            app.UseMiddleware<ThunderAuthenticationMiddleware>();
            app.UseRouting();
            app.UseThunderCors();

            // app.UseThunderUnitOfWork();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseThunderSwagger(ApiName, "v1");
        }
    }
}
