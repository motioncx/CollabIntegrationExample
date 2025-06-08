namespace CollabIntegrationExample.WebApp.Command.Reports.Interfaces;

public interface ILimitOffsetQuery
{
    /// <summary>
    /// Number of results to return from a query.
    /// </summary>
    public int? Limit { get; }
        
    /// <summary>
    /// Where to start the results of a query.
    /// </summary>
    public int? Offset { get; }
}