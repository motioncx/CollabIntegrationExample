using System;

namespace CollabIntegrationExample.WebApp.Command.Reports.Domain;

public class InteractionResultModels
{

    public ulong Count_id { get; set; }

    public ulong AgentId { get; set; }
    public DateTimeOffset? EndedQuarterHourUtc { get; set; }
    public int SumAnsweredApplied { get; set; }
    
    
    public decimal SumAgentHandleTime { get; set; }
    public decimal SumAcceptTime { get; set; }
    
    public int SumServiceLevelApplies { get; set; }
    public int SumIsInServiceLevelApplied { get; set; }
    
    public decimal SumTotalTalkTime { get; set; }
    public decimal SumTotalHoldTime { get; set; }
    
    public int CountTotalACWTime { get; set; }
    public decimal SumTotalACWTime { get; set; }
        public InteractionResultModels() {}
}