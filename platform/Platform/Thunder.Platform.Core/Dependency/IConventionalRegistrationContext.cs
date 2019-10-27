using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Thunder.Platform.Core.Dependency
{
    /// <summary>
    /// Used to pass needed objects on conventional registration process.
    /// </summary>
    public interface IConventionalRegistrationContext
    {
        Assembly Assembly { get; }

        IServiceCollection Services { get; }
    }
}
