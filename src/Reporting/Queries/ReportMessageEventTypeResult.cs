using System.Collections.Generic;

namespace CollabIntegrationExample.WebApp.Command.Reports.Queries;


public class ReportMessageEventTypesResult {
    public Dictionary<int, string> EventTypes { get; set; }
    public Dictionary<int, string> EventTypeReasons { get; set; }
}