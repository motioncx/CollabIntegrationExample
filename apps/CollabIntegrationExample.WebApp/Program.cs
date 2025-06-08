using BelkSnowFlakeServiceProxies;

using CollabIntegrationExample.WebApp.Config;
using CollabIntegrationExample.WebApp.Services;

using Microsoft.AspNetCore.Rewrite;
using MotionServiceProxies;
using Newtonsoft.Json;
using Telemetry;

var builder = WebApplication.CreateBuilder(args);
var apiDetails = ConfigDetails.GetAPIs.ReadFromJsonFile(builder.Configuration);

// Configure services
ConfigureServices(builder.Services);

// just adding some level of logs to assist anyone trying to run and undestand this project


var app = builder.Build();

// Configure middleware
ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services)
{
    var motionUrl = apiDetails.UAT.MotionApiUrl;
    var motionApiKey = apiDetails.UAT.MotionApiKey;
    services.AddControllers().AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
    });
    var testIt = AppNameHelper.GetExecutingAssembly();
    var appName = typeof(Program).Assembly;
    
    
    services.SetupSnowflakeProxies(apiDetails.UAT.BelkStore.User, apiDetails.UAT.BelkStore.Password, apiDetails.UAT.BelkBaseUrl);
    services.SetupMotionProxies(motionApiKey, motionUrl);
    services.AddTelemetry();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
    services.AddTransient<ReportingServiceProxy>();
    services.AddTransient<CrmServiceProxy>();
    services.AddTransient<InteractionServiceProxy>();

}

void ConfigureMiddleware(WebApplication app)
{
    app.UseTelemetry<Program>(builder.Configuration, LogLevel.Debug);
    MotionLog.Warning("Testing log");
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();

    var option = new RewriteOptions();
    option.AddRedirect("^$", "/swagger");
    app.UseRewriter(option);

    app.MapControllers();
}