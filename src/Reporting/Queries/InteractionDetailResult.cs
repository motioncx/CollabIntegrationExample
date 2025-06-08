using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace CollabIntegrationExample.WebApp.Command.Reports.Queries;

public class InteractionDetailResult
{
    public JObject Interaction { get; set; }
    public IEnumerable<JObject>  Messages  { get; set; }
    public IEnumerable<JObject> WorkflowMessages { get; set; }
}

