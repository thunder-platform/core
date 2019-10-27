using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Thunder.Platform.AspNetCore.Context;
using Thunder.Platform.AspNetCore.ExceptionHandling;
using Thunder.Platform.AspNetCore.UnitOfWork;
using Thunder.Platform.AspNetCore.Validation;
using Thunder.Platform.Core;
using Thunder.Platform.Core.Context;
using Thunder.Platform.Core.Modules;

[assembly: ThunderModuleAssembly]

namespace Thunder.Platform.AspNetCore
{
    [DependsOn(typeof(ThunderKernelModule))]
    public class ModuleInit : ThunderModule
    {
        protected override void InternalRegisterTo(IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<ThunderExceptionFilter>();
            services.AddScoped<ThunderModelValidationFilter>();
            services.AddScoped<ThunderUowActionFilter>();

            // By default, the thunder system will use ThreadStatic to store context data.
            // When it comes to AspNetCore, the system will use HttpContext instead.
            services.Replace(ServiceDescriptor.Singleton(provider =>
            {
                UserContext.Current.SetUserContextStore(new ThunderWebUserContextStore(provider.GetService<IHttpContextAccessor>()));

                return UserContext.Current;
            }));
        }
    }
}
