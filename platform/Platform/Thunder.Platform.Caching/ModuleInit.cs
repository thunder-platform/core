using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.Caching.Memory;
using Thunder.Platform.Caching.Redis;
using Thunder.Platform.Core;
using Thunder.Platform.Core.Modules;

[assembly: ThunderModuleAssembly]

namespace Thunder.Platform.Caching
{
    [DependsOn(typeof(ThunderKernelModule))]
    public class ModuleInit : ThunderModule
    {
        protected override void InternalRegisterTo(IServiceCollection services)
        {
            services.AddOptions<ThunderRedisCacheOptions>();
            services.AddSingleton<ICacheRepository>(new MemoryCacheRepository("ThunderDefaultCache"));
            services.AddTransient<ICacheDataProvider, CacheDataProvider>();
            services.AddTransient<IRedisAccessorProvider, RedisAccessorProvider>();
        }
    }
}
