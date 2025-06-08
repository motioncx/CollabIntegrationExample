using System.Collections.Generic;
using CollabIntegrationExample.WebApp.Command.Reports.Interfaces;

namespace CollabIntegrationExample.WebApp.Command.Reports.Queries;
public class WorkflowMessageEventTypesLookupQuery: IReportingRequest<WorkflowMessageEventTypesResult>
{
}
public class WorkflowMessageEventTypesResult {
    public Dictionary<int, string> EventTypes { get; set; }
}