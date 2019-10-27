using Microsoft.Extensions.Configuration;
using Thunder.Platform.Core.Domain.UnitOfWork.Abstractions;
using Thunder.Platform.Core.Exceptions;

namespace Thunder.Platform.Core.Domain.UnitOfWork
{
    public class DefaultConnectionStringResolver : IConnectionStringResolver
    {
        private readonly IConfiguration _configuration;

        public DefaultConnectionStringResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public virtual string GetNameOrConnectionString(ConnectionStringResolveArgs args)
        {
            Guard.NotNull(args, nameof(args));

            var defaultConnectionString = _configuration.GetConnectionString("Default");
            if (!string.IsNullOrWhiteSpace(defaultConnectionString))
            {
                return defaultConnectionString;
            }

            throw new GeneralException("Could not find a connection string definition for the application. Set IAbpStartupConfiguration.DefaultNameOrConnectionString or add a 'Default' connection string to application .config file.");
        }
    }
}
