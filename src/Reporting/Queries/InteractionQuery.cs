using System;
using System.Collections.Generic;
using System.ComponentModel;
using CollabIntegrationExample.WebApp.Command.Reports.Filtering;
using CollabIntegrationExample.WebApp.Command.Reports.Interfaces;
using CollabIntegrationExample.WebApp.Command.Reports.TypeDefinitions;
using Newtonsoft.Json.Linq;

namespace CollabIntegrationExample.WebApp.Command.Reports.Queries;


public class InteractionFilterBase: IFilteredQuery, IDateRangeQuery
{
    [DefaultValue(null)]
    public int[] Queues { get; set; }
   
    [DefaultValue(false)]
    public bool ExcludeNonqueued { get; set; }
    public JObject Filter { get; set; }
    
    [DefaultValue(null)]
    public DateTimeOffset? StartDateTime { get; set; }
    [DefaultValue(null)]
    public DateTimeOffset? EndDateTime { get; set; }
    
    [DefaultValue(-2)]
    public int? RelativePeriodStart { get; set; }
    
    [DefaultValue(-1)]
    public int? RelativePeriodEnd { get; set; }
    
    [DefaultValue("America/New_York")]
    public string RelativeTimezone { get; set; }
    
    [DefaultValue(null)]
    public string DateFilterField { get; set; }
    
    [DefaultValue(TimeResolution.QuarterHour)]
    public TimeResolution? Resolution { get; set; }
    
}


public class InteractionAggregateFilter : InteractionFilterBase
{
    public string[] GroupFields { get; set; }
    public Dictionary<string, AggregateTypes[]> Aggregates { get; set; }
}

public class InteractionFilter : InteractionFilterBase, IDateRangeQuery, ILimitOffsetQuery
{
    [DefaultValue(null)]
    public string[] Fields { get; set; }
   
    [DefaultValue(null)]
    public string[] OrderBy { get; set; }
    
    [DefaultValue(5)]
    public int? Limit { get; set; }
    [DefaultValue(0)]
    public int? Offset { get; set; }
}

public class InteractionQuery: BaseQuery<InteractionFilter, IEnumerable<JObject>>
{
    public const int MaximumResults = 500;
    
    public InteractionQuery(InteractionFilter request) : base( request)
    {
    }
}