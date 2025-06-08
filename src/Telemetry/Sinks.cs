using System.Diagnostics;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Enrichers.CallerInfo;
using Serilog.Enrichers.OpenTelemetry;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.EntityFrameworkCore.Destructurers;
using Serilog.Exceptions.Refit.Destructurers;
using Serilog.Exceptions.SqlServer.Destructurers;
using Serilog.Sinks.Grafana.Loki;

namespace Telemetry;


public class Sinks
{
    const string outputTemplate = "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] [{TraceId}] ({SourceContext}) {Message:lj} {Properties}{NewLine}{Exception}";

    public static void UseConsole(IConfiguration config)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .WriteTo.Console(outputTemplate: outputTemplate)
            
  
            .WriteTo.File("Logs/logs.log")
            .Enrich.FromLogContext()
            .Enrich.WithOpenTelemetryTraceId()
            .Enrich.WithOpenTelemetrySpanId()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Default", LogEventLevel.Information)
            .MinimumLevel.Override( "Microsoft", LogEventLevel.Warning )

            .CreateLogger();
    }

    public static void UseLoki<T>( LogLevel logLevel = LogLevel.Warning )
    {
        string appName = AppNameHelper.GetExecutingAssembly();
        
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")!;
        string assemblyPrefix = typeof(T).Assembly.FullName?.Split(',')[0] ?? "Motion";
        IEnumerable<LokiLabel> labels = [
            new LokiLabel { Key = "service", Value =appName },
            new LokiLabel { Key = "environment", Value = environment },
            new LokiLabel { Key = "host", Value = Environment.MachineName },
        ];
        LogEventLevel minimumLevel = Enum.Parse<LogEventLevel>(logLevel.ToString());

        // TODO: setup dynamic logs
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Is(minimumLevel)
            .Enrich.FromLogContext()
            .Enrich.WithOpenTelemetryTraceId()
            .Enrich.WithOpenTelemetrySpanId()
            .Enrich.WithMachineName()
            .Enrich.WithThreadId()
            .Enrich.WithProcessId()
            .Enrich.WithProcessName()

            .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                .WithDefaultDestructurers()
                .WithDestructurers([
                    new DbUpdateExceptionDestructurer(),
                    new ApiExceptionDestructurer(destructureHttpContent: true),
                    new SqlExceptionDestructurer(),
                ]))
            // TODO: remove it once grafana is stable and elk is removed
            .Enrich.WithProperty("Motion.Application", appName)
            .Enrich.WithProperty("Motion.Environment", environment)
            .WriteTo.Console(outputTemplate: outputTemplate)
            
            // if this was a true service, i wouldn't write a file log and would use grafana/loki
            .WriteTo.File("Logs/logs.log")
            .CreateLogger();
        // TODO: test this code with load testing
        //.WriteTo.Async(a => a.OpenTelemetry(options =>
        //{
        //    options.Endpoint = "http://localhost:4317"; // Tempo OTLP endpoint
        //    options.BatchSize = 50;  // Send logs immediately if batch reaches 50
        //    options.Period = TimeSpan.FromSeconds(2); // Otherwise, wait max 2 seconds
        //}))
        // .WriteTo.GrafanaLoki(
        //     GlobalSettings.Settings.Telemetry.Logs.Url,
        //     labels: labels,
        //     period: TimeSpan.FromSeconds(GlobalSettings.Settings.Telemetry.Logs.Period),
        //     batchPostingLimit: GlobalSettings.Settings.Telemetry.Logs.BatchPostingLimit)
        // .CreateLogger();
    }

    public static string? GetVersion<T>()
    {
        // TODO: is APP_VERSION correct?
        string? version = Environment.GetEnvironmentVariable("APP_VERSION");

        if (version is not null)
            return version;

        var assembly = typeof(T).Assembly;
        var fvi = FileVersionInfo.GetVersionInfo(assembly.Location);

        return fvi.FileVersion;
    }
}
