using CollabIntegrationExample.WebApp.Command.Reports.Queries;
using CollabIntegrationExample.WebApp.Command.Reports.ReportResponses;
using MotionServiceProxies.HttpProxy;
using Newtonsoft.Json.Linq;
namespace CollabIntegrationExample.WebApp.Services;

public class ReportingServiceProxy 
{


    private ILogger<ReportingServiceProxy> _logger;
    private IHttpContextAccessor _httpContext;
    private IReportingApiServiceProxy _reportServiceProxy;

    public ReportingServiceProxy(IReportingApiServiceProxy reportApiProxy, IHttpContextAccessor httpContext, ILogger<ReportingServiceProxy> logger) 
      
    {
        _reportServiceProxy = reportApiProxy;
        _logger = logger;
        _httpContext = httpContext;

    }

    public async Task<ReportApiResp>  GetInteractionAggregate(InteractionAggregateFilter filter)
    {
    
        try
        {
            var resp = await _reportServiceProxy.InteractionAggregates(filter);
            if (resp.IsSuccessful)
            {
                return resp.Content;
            }
            else
            {
                throw new Exception("Exception occurred");
            }

        }
        catch (Exception ex)
        {
            throw new Exception("Exception occurred");
        }
    }
    public async Task<JObject>  LookupReportMessageEventTypes()
    {
        var resp = await _reportServiceProxy.LookupReportMessageEventTypes();
        if (resp.IsSuccessful)
        {
            return resp.Content;
        }
        else
        {
            throw new Exception("Exception occurred");
        }
    }
    
    public async Task<JObject>  LookupWorkflowMessageEventTypes()
    {

            var resp = await _reportServiceProxy.LookupWorkflowMessageEventTypes();
            if (resp.IsSuccessful)
            {
                return resp.Content;
            }
            else
            {
                throw new Exception("Exception occurred");
            }


    }
    
    public async Task<JObject>  LookupAgents(bool includeDisabled)
    {

        try
        {
            var resp = await _reportServiceProxy.LookupAgents(includeDisabled);
            if (resp.IsSuccessful)
            {
                return resp.Content;
            }
            else
            {
                throw new Exception("Exception occurred");
            }

        }
        catch (Exception ex)
        {
            throw new Exception("Exception occurred");
        }
    }
    
    public async Task<JObject>  InteractionMessages(InteractionIdFilter filter)
    {

        try
        {
            var resp = await _reportServiceProxy.InteractionMessages(filter);
            if (resp.IsSuccessful)
            {
                return resp.Content;
            }
            else
            {
                throw new Exception("Exception occurred");
            }

        }
        catch (Exception ex)
        {
            throw new Exception("Exception occurred");
        }
    }
}