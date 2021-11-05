using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
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
                .UseSerilog((context, config)
                    => config.ReadFrom.Configuration(context.Configuration, "Logging"),
                    preserveStaticLogger: true)
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<Runner>();
                })
                .Build();
            return host.RunAsync();
        }
    }
}
