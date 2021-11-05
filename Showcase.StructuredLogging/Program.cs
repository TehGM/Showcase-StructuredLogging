using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Threading.Tasks;

namespace TehGM.Showcase.StructuredLogging
{
    class Program
    {
        static Task Main(string[] args)
        {
            // create a generic host
            IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    // load secrets files if present
                    config.AddJsonFile("appsecrets.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile($"appsecrets.{context.HostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true);
                })
                .UseSerilog((context, config) =>
                {
                    config.ReadFrom.Configuration(context.Configuration, "Logging");

                    DatadogOptions ddOptions = context.Configuration.GetSection("Logging").GetSection("DataDog").Get<DatadogOptions>();
                    if (!string.IsNullOrWhiteSpace(ddOptions?.ApiKey))
                    {
                        config.WriteTo.DatadogLogs(
                            ddOptions.ApiKey,
                            source: ".NET",
                            service: ddOptions.ServiceName,
                            host: ddOptions.HostName ?? Environment.MachineName,
                            new string[] {
                                $"env:{ddOptions.EnvironmentName ?? context.HostingEnvironment.EnvironmentName}"
                            },
                            ddOptions.ToDatadogConfiguration()
                        );
                    }
                })
                .ConfigureServices((context, services) =>
                {
                    services.Configure<RunnerOptions>(context.Configuration);
                    services.AddHostedService<Runner>();
                })
                .Build();
            return host.RunAsync();
        }
    }
}
