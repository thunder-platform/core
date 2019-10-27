using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.Core.Modules;

[assembly: ThunderModuleAssembly]

namespace Thunder.Platform.Caching.Tests
{
    [StartupModule]
    public class ModuleInit : ThunderModule
    {
        protected override void InternalRegisterTo(IServiceCollection services)
        {
        }
    }
}
