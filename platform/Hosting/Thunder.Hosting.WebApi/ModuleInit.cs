using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.Core.Logging;
using Thunder.Platform.Core.Modules;

[assembly: ThunderModuleAssembly]

namespace Thunder.Hosting.WebApi
{
    [StartupModule]
    public class ModuleInit : ThunderModule
    {
        protected override void InternalRegisterTo(IServiceCollection services)
        {
            // builder.AddJsonFile("logging.config.json");
            services.AddOptions<ThunderLoggingOptions>();
        }
    }
}
