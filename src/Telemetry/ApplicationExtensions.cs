using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Telemetry;


public static class ApplicationExtensions
{
    public static IApplicationBuilder UseTelemetry<T>(this IApplicationBuilder application, IConfiguration config,  LogLevel logLevel = LogLevel.Warning)
    {
        UseLogs<T>(config, logLevel);
        MotionLog.Information("Application started");
        // this is just an exampple, therefore, not using opentel/prometheue/grafana
        // application.UseOpenTelemetryPrometheusScrapingEndpoint();
        return application;
    }

    private static void UseLogs<T>(IConfiguration config, LogLevel logLevel = LogLevel.Warning)
    {
        if (Environment.GetEnvironmentVariable("DISABLE_TELEMETRY") == "True")
        {
            Sinks.UseConsole(config);
            return;
        }
        Sinks.UseLoki<T>(logLevel);
    }
}
