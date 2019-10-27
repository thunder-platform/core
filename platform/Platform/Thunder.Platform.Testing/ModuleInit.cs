using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.Core;
using Thunder.Platform.Core.Modules;

[assembly: ThunderModuleAssembly]

namespace Thunder.Platform.Testing
{
    [DependsOn(typeof(ThunderKernelModule))]
    public class ModuleInit : ThunderModule
    {
        protected override void InternalRegisterTo(IServiceCollection services)
        {
        }
    }
}
