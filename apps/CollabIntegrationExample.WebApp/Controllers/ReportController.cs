using CollabIntegrationExample.WebApp.Command.Reports.Queries;
using CollabIntegrationExample.WebApp.Command.Reports.ReportResponses;
using CollabIntegrationExample.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Refit;

namespace CollabIntegrationExample.WebApp.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController(ReportingServiceProxy serviceProxy) : ControllerBase
{
    private ReportingServiceProxy _serviceProxy = serviceProxy;
    

    [HttpGet]
    [Route("interaction/aggregate/example")]
    public async  Task<IActionResult> InteractionReportExample()
    {
        
        var jsonDetails = @"{
              ""startDateTime"": ""2025-01-21T05:00:00.000Z"",
              ""endDateTime"": ""2025-01-22T04:59:59.999Z"",
              ""dateFilterField"": ""StartedDateTimeUtc"",
              ""groupFields"": [
                ""AgentId"",
                ""StartedQuarterHourUtc""
              ],
              ""aggregates"": {
                ""_id"": [
                  ""Count""
                ]
              },
              ""queues"": [],
              ""filter"": {
                ""ChannelType"": {
                  ""$ne"": 0
                },
                ""AgentId"": {
                  ""$ne"": null
                },
                ""Direction"": {
                  ""$ne"": 1
                }
              },
              ""relativeTimezone"": ""America/New_York""
            }";

        var filter = JsonConvert.DeserializeObject<InteractionAggregateFilter>(jsonDetails);

        
        var resp = await _serviceProxy.GetInteractionAggregate(filter);
        return Ok(resp);
        

    }
    
    
    [HttpPost]
    [Route("interaction/messages")]
    public async Task<IActionResult> InteractionMessages(
        CancellationToken cancellationToken,
        [FromBody] InteractionIdFilter filter)
    {
        var resp = await _serviceProxy.InteractionMessages(filter);
        return Ok(resp);
    }

    
    [HttpGet]
    [HttpPost]
    [Route("lookup/agents")]
    public async Task<IActionResult> LookupAgents([FromQuery] bool includeDisabled = true)
    {
        var resp = await _serviceProxy.LookupAgents(includeDisabled);
        return Ok(resp);
    }
    
    [HttpGet]
    [HttpPost]
    [Route("lookup/workflow-message-event-types")]
    public async Task<IActionResult> LookupWorkflowMessageEventTypes()
    {
        var resp = await _serviceProxy.LookupWorkflowMessageEventTypes();
        return Ok(resp);
        //return await this.Mediator.Send(new WorkflowMessageEventTypesLookupQuery());
    }
    
    [HttpGet]
    [HttpPost]
    [Route("lookup/report-message-event-types")]
    public async Task<IActionResult> LookupReportMessageEventTypes()
    {
        var resp = await _serviceProxy.LookupReportMessageEventTypes();
        return Ok(resp);
        //return await this.Mediator.Send(new ReportMessageEventTypesLookupQuery());
    }


    
    [HttpPost]
    [Route("interaction/aggregates")]
    public async Task<ReportApiResp> InteractionList(
        [FromBody] InteractionAggregateFilter filter)
    {
        var resp = await _serviceProxy.GetInteractionAggregate(filter);
        return resp;
    }
}