using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Thunder.Platform.Core.Logging;
using Thunder.Platform.ThunderBus.Options;

namespace Thunder.Hosting.TestMq
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging((context, builder) =>
                {
                    var thunderLoggingOptions = new ThunderLoggingOptions();
                    context.Configuration.GetSection(nameof(ThunderLoggingOptions)).Bind(thunderLoggingOptions);

                    var logger = LoggerConfig.Config(thunderLoggingOptions);
                    builder.AddSerilog(logger);

                    // To allow injection of Serilog.ILogger in constructor.
                    builder.Services.AddSingleton(logger);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions<RabbitMqOptions>().Bind(hostContext.Configuration.GetSection("RabbitMqOptions"));

                    services.AddThunderModuleSystem();
                    services.AddThunderBusConsumers();

                    services.AddHostedService<ThunderBusHostedService>();
                });
    }
}
