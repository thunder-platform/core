using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace Thunder.Platform.Testing
{
    public abstract class BaseThunderTest
    {
        protected BaseThunderTest()
        {
            Configuration = CreateInMemoryConfiguration();
            Log.Logger = CreateLogger();

            Provider = CreateAppServiceCollection()
                .AddSingleton(Log.Logger)
                .BuildServiceProvider();
        }

        public IServiceProvider Provider { get; }

        protected IConfiguration Configuration { get; }

        protected virtual IEnumerable<KeyValuePair<string, string>> SetupInMemoryConfiguration()
        {
            return new KeyValuePair<string, string>[0];
        }

        protected virtual ILogger SetupLoggerConfiguration()
        {
            const string logFormat = "{NewLine}{Timestamp:HH:mm:ss} [{Level}] ({CorrelationToken}) {Message}{NewLine}{Exception}";
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .MinimumLevel.Debug()
                .WriteTo.Console(LogEventLevel.Verbose, logFormat)
                .CreateLogger();
        }

        protected virtual IServiceCollection SetupAppServices(IServiceCollection services)
        {
            return services;
        }

        private IConfigurationRoot CreateInMemoryConfiguration()
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(SetupInMemoryConfiguration())
                .Build();

            return configuration;
        }

        private ILogger CreateLogger()
        {
            return SetupLoggerConfiguration();
        }

        private IServiceCollection CreateAppServiceCollection()
        {
            var serviceCollection = new ServiceCollection()
                .AddThunderModuleSystem()
                .AddSingleton<IConfiguration>(provider => CreateInMemoryConfiguration());

            SetupAppServices(serviceCollection);

            return serviceCollection;
        }
    }
}
