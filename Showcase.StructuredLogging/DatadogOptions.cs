using Serilog.Sinks.Datadog.Logs;

namespace TehGM.Showcase
{
    public class DatadogOptions
    {
        public string ServiceName { get; set; } = "Showcase-StructuredLogging";
        public string EnvironmentName { get; set; }
        public string HostName { get; set; }
        public string ApiKey { get; set; }

        public string URL { get; private set; } = "intake.logs.datadoghq.com";
        public int Port { get; private set; } = 10516;
        public bool UseSSL { get; private set; } = true;
        public bool UseTCP { get; private set; } = true;


        public DatadogConfiguration ToDatadogConfiguration()
            => new DatadogConfiguration(URL, Port, UseSSL, UseTCP);
    }
}
