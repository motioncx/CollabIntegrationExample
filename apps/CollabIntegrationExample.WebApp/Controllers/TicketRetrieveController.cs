

using CollabIntegrationExample.WebApp.Command.Reports.Queries;
using CollabIntegrationExample.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using MotionServiceProxies.Command.Query;
using Shared.Models;

namespace CollabIntegrationExample.WebApp.Controllers;


[ApiController]
[Route("[controller]")]
public class TicketRetrieveController(
        CrmServiceProxy crmServiceProxy, 
        InteractionServiceProxy interactionServiceProxy,
        ReportingServiceProxy reportingServiceProxy
        ): ControllerBase
{
    private CrmServiceProxy _crmServiceProxy = crmServiceProxy;
    private InteractionServiceProxy _interactionServiceProxy = interactionServiceProxy;
    private ReportingServiceProxy _reportingServiceProxy = reportingServiceProxy;



    [HttpGet]
    [Route("get-ticket-with-events")]
    public async Task<IActionResult> GetTicketWithEvents()
    {
        
        
        // retrieve ticket
        var tickets = await _crmServiceProxy.GetTickets(new ReadTicketsQuery()
        {
            TenantId = 3, CreatedAt = new DateTime(2024, 10, 11), CreatedTo = new DateTime(2024, 10, 15),
            MaxResultCount = 5, SkipCount = 0,

        });

        // ensure we have a ticket... and get first one
        
        foreach(var ticket in tickets.Items)
        {
            // we need more info, as dont have metadata needed to know interactions'
            // this is because "Tickets" request is limited in the amount of data that 
            // is sent back.  you have to call additional data with specific ticket info
            // to retrieve additional info needed
            // making request to get additional ticket info
            var moreTicketInfo = await _crmServiceProxy.GetTicketInfo(new ReadTicketQueryCommand()
            {
                TicketId = ticket.Id, TenantId = ticket.TenantId
            });
            
            // ticvket should have metadata, if not, iterate through to the next
            
            var metaData = await _crmServiceProxy.GetTicketMetaInfo(moreTicketInfo);
            if (metaData == null || metaData.Interactions.Count <= 0)
            {
                
            }
            foreach (var interactions in metaData.Interactions)
            {
                // now, we canm call interactions for data and/or call reporting
                var getReportingItems = await _reportingServiceProxy.InteractionMessages(new InteractionIdFilter()
                {
                    InteractionId = interactions
                });

                var callInteractions = await _interactionServiceProxy.GetTranscript(interactions);


                TicketExampleData ticketExampleData = new TicketExampleData();
                ticketExampleData.InteractionMetaData = metaData;
                ticketExampleData.Ticket = ticket;
                ticketExampleData.InteractionMessageEvents = callInteractions;
                ticketExampleData.ReportingMessageEvents = getReportingItems;
                
                return Ok(ticketExampleData);
            }
        }
        
        
        return Ok();
    }
    

    [HttpGet]
    [Route("get-tickets")]
    public async Task<IActionResult> GetTickets()
    {
        var ticketInfo = await _crmServiceProxy.GetTickets(new ReadTicketsQuery()
        {
            TenantId = 3, CreatedAt = new DateTime(2024, 10, 11), CreatedTo = new DateTime(2024, 10, 15), MaxResultCount = 25, SkipCount = 0,
            
        });
        return Ok(ticketInfo);
    }
    
    [HttpGet]
    [Route("get-ticket")]
    public async Task<IActionResult> GetTicket(int tenantId, long ticketId)
    {
        var ticketInfo = await _crmServiceProxy.GetTicketInfo(
            new ReadTicketQueryCommand()
            {
                TicketId = ticketId, TenantId = tenantId
            }
        );

        return Ok(ticketInfo);
        
    }

}