using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Thunder.Platform.Core.Logging;
using Thunder.Platform.Core.Timing;

namespace Thunder.Hosting.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Setting clock provider, using utc time
            Clock.SetProvider(new UtcClockProvider());

            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureHostConfiguration(builder =>
                {
                    builder.AddJsonFile("thundersettings.json", optional: false, reloadOnChange: false);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder
                        .ConfigureKestrel(serverOptions =>
                        {
                            serverOptions.AddServerHeader = false;
                        })
                        .ConfigureLogging((context, builder) =>
                        {
                            var thunderLoggingOptions = new ThunderLoggingOptions();
                            context.Configuration.GetSection(nameof(ThunderLoggingOptions)).Bind(thunderLoggingOptions);

                            var logger = LoggerConfig.Config(thunderLoggingOptions);
                            builder.AddSerilog(logger);

                            // To allow injection of Serilog.ILogger in constructor.
                            builder.Services.AddSingleton(logger);
                        })
                        .ConfigureServices(collection => collection.AddThunderModuleSystem())
                        .UseStartup<Startup>();
                });
    }
}
