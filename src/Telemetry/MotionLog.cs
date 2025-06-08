using Newtonsoft.Json.Linq;
using Serilog;
using Serilog.Context;

namespace Telemetry;

public static class MotionLog
{
    public static void Verbose(string messageTemplate, params object[] propertyValues) => Log.Verbose(messageTemplate, propertyValues);
    public static void Verbose(Exception exception, string messageTemplate, params object[] propertyValues) => Log.Verbose(exception, messageTemplate, propertyValues);
    public static void Debug(string messageTemplate, params object[] propertyValues) => Log.Debug(messageTemplate, propertyValues);
    public static void Debug(Exception exception, string messageTemplate, params object[] propertyValues) => Log.Debug(exception, messageTemplate, propertyValues);
    public static void Information(string messageTemplate, params object[] propertyValues) => Log.Information(messageTemplate, propertyValues);
    public static void Information(Exception exception, string messageTemplate, params object[] propertyValues) => Log.Information(exception, messageTemplate, propertyValues);
    public static void Warning(string messageTemplate, params object[] propertyValues) => Log.Warning(messageTemplate, propertyValues);
    public static void Warning(Exception exception, string messageTemplate, params object[] propertyValues) => Log.Warning(exception, messageTemplate, propertyValues);
    public static void Error(string messageTemplate, params object[] propertyValues) => Log.Error(messageTemplate, propertyValues);
    public static void Error(Exception exception, string messageTemplate, params object[] propertyValues) => Log.Error(exception, messageTemplate, propertyValues);
    public static void Fatal(string messageTemplate, params object[] propertyValues) => Log.Fatal(messageTemplate, propertyValues);
    public static void Fatal(Exception exception, string messageTemplate, params object[] propertyValues) => Log.Fatal(exception, messageTemplate, propertyValues);

    public static IDisposable AddAgentId(object value) => LogContext.PushProperty("agentId", value);
    public static IDisposable AddChannelUniqueIdentifier(object value) => LogContext.PushProperty("channelUniqueIdentifier", value);
    public static IDisposable AddInteractionId(object value) => LogContext.PushProperty("interactionId", value);
    public static IDisposable AddQueueId(object value) => LogContext.PushProperty("queueId", value);
    public static IDisposable AddSupportPortalId(object value) => LogContext.PushProperty("supportPortalId", value);
    public static IDisposable AddTenantId(object value) => LogContext.PushProperty("tenantId", value);
    public static IDisposable AddTicketId(object value) => LogContext.PushProperty("ticketId", value);
    public static IDisposable AddWorkflowId(object value) => LogContext.PushProperty("workflowId", value);
    public static IDisposable AddWorkItemId(object value) => LogContext.PushProperty("workItemId", value);
    public static IDisposable AddListener(object value) => LogContext.PushProperty("listener", value);
    public static IDisposable AddChannelType(object value) => LogContext.PushProperty("channelType", value);
    public static IDisposable AddChannelProviderType(object value) => LogContext.PushProperty("channelProviderType", value);
    public static IDisposable AddClientIdentifier(object value) => LogContext.PushProperty("clientIdentifier", value);
    public static IDisposable AddValue(string key, object value) => LogContext.PushProperty(key, value);
    public static IDisposable? AddObject(string key, object value)
    {
        try
        {
            return LogContext.PushProperty(key, JToken.FromObject(value).ToString());
        }
        catch (Exception)
        {
            return default;
        }
    }
}
