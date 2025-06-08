using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Telemetry;

public static class BuilderExtensions
{
    public static IServiceCollection AddTelemetry(this IServiceCollection services)
    {


        AddMetrics(services);
        AddTraces(services);

        return services;
    }

    private static void AddMetrics(IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .WithMetrics(metrics =>
            {
                metrics
                    .AddMeter(MetricConstants.All)
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddRuntimeInstrumentation()
                   //.AddPrometheusExporter()
                   // .AddOtlpExporter(o => o.Endpoint = new Uri(GlobalSettings.Settings.Telemetry.Metrics.Url));
                   ;
            });
    }

    private static void AddTraces(IServiceCollection services)
    {
        services.AddOpenTelemetry()
            .WithTracing(tracing =>
            {
                tracing
                    .AddSource(TraceConstants.All)
                    //.SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(GlobalSettings.Settings.AppSettings.Name))
                    .AddAspNetCoreInstrumentation()
                    .AddHttpClientInstrumentation()
                    .AddEntityFrameworkCoreInstrumentation(options => options.SetDbStatementForText = true);
                    //.AddOtlpExporter(o => o.Endpoint = new Uri(GlobalSettings.Settings.Telemetry.Traces.Url));
            });
    }
}
