using System;
using Microsoft.Extensions.DependencyInjection;

namespace Thunder.Platform.Core.Modules
{
    public interface IThunderModuleManager
    {
        void Initialize(IServiceCollection services);
    }
}
