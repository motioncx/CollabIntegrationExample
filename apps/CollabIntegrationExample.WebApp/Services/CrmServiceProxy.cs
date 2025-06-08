using MotionServiceProxies.Command.Query;
using MotionServiceProxies.HttpProxy;
using Newtonsoft.Json;
using Refit;
using Shared.Models.CRM;
using Shared.Models.IR;
using Shared.Models.IR.Helper;

namespace CollabIntegrationExample.WebApp.Services;

public class CrmServiceProxy 
{

    private ILogger<CrmServiceProxy> _logger;
    private IHttpContextAccessor _httpContext;
    
    private ITicketServiceProxy _ticketServiceProxy;


    public CrmServiceProxy(
        ITicketServiceProxy ticketServiceProxy,
        IHttpContextAccessor httpContext, 
        ILogger<CrmServiceProxy> logger) 
        
    {
        _ticketServiceProxy = ticketServiceProxy;
        _logger = logger;
        _httpContext = httpContext;
    }

    public async Task<TicketQueryResponse> GetTickets(ReadTicketsQuery query)
    {
        

        // if you want to get details on ticket(s)
        var getTicketsTest = await _ticketServiceProxy.GetVTicketsAsync(query);
        if (getTicketsTest.IsSuccessful)
        {
            return getTicketsTest.Content;
        }
        else
        {
            throw new Exception("Unable to retrieve ticket info!");
        }
    }

    public async Task<TicketDto?> GetTicketInfo(ReadTicketQueryCommand query)
    {


        // call specific ticket id
        var getTicketInfo = await _ticketServiceProxy.GetV1TicketAsync(query);

        // check if success
        if (getTicketInfo.IsSuccessStatusCode)
        {
            // get details to get interactionid
            return getTicketInfo.Content;
        }

        return null;
    }

    public async Task<InteractionMetaData?> GetTicketMetaInfo(TicketDto ticketInfo)
    {
        var getInfo = ticketInfo.IRWorkItemInfoDetails;
        var irMetaData = HelperIrMetaData.DeserializeInteractionMetaData(getInfo);
        
        return irMetaData;
    }
    

}