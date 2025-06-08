using CollabIntegrationExample.WebApp.Command.Reports.Queries;
using CollabIntegrationExample.WebApp.Command.Reports.ReportResponses;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Shared.Models.IR;
using Refit;

namespace MotionServiceProxies.HttpProxy;

public interface IReportingApiServiceProxy
{
    [Post("/reporting/api/v1/interaction/aggregates")]
    Task<ApiResponse<ReportApiResp>> InteractionAggregates(
        InteractionAggregateFilter filter,
        CancellationToken cancellationToken = default (CancellationToken));
    
    
    [Post("/reporting/api/v1/interaction/messages")]
    Task<ApiResponse<JObject>> InteractionMessages(
        InteractionIdFilter filter,
        CancellationToken cancellationToken = default (CancellationToken));
    
    [Post("/reporting/api/v1/lookup/workflow-message-event-types")]
    Task<ApiResponse<JObject>> LookupWorkflowMessageEventTypes(
        CancellationToken cancellationToken = default (CancellationToken));
    
    [Post("/reporting/api/v1/lookup/report-message-event-types")]
    Task<ApiResponse<JObject>> LookupReportMessageEventTypes(
        CancellationToken cancellationToken = default (CancellationToken));

        
    [Post("/reporting/api/v1/lookup/agents")]
    Task<ApiResponse<JObject>> LookupAgents(
        [FromQuery]bool includeDisabled = true,
        CancellationToken cancellationToken = default (CancellationToken));
}
