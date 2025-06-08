using Newtonsoft.Json.Linq;
using Shared.Models.CRM;
using Shared.Models.IR;

namespace Shared.Models;

public class TicketExampleData
{
    public TicketDto Ticket { get; set; }
    public InteractionMetaData InteractionMetaData { get; set; }
    public JObject? ReportingMessageEvents { get; set; }
    public TranscriptResult? InteractionMessageEvents { get; set; }
}