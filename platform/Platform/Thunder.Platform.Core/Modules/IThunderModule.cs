using Microsoft.Extensions.DependencyInjection;

namespace Thunder.Platform.Core.Modules
{
    public interface IThunderModule
    {
        void RegisterTo(IServiceCollection services);
    }
}
