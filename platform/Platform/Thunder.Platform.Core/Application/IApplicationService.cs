using Thunder.Platform.Core.Dependency;

namespace Thunder.Platform.Core.Application
{
    /// <summary>
    /// This interface must be implemented by all application services to identify them by convention.
    /// </summary>
    public interface IApplicationService : ITransientInstance
    {
    }
}
