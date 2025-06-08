using CollabIntegrationExample.WebApp.Command.Reports.Interfaces;

namespace CollabIntegrationExample.WebApp.Command.Reports;

public abstract class BaseQuery<TQuery, TResponse>: IReportingRequest<TResponse>
{
    #region Properties
        
    
    public TQuery Request { get; }

    #endregion

    #region Constructors/Destructors
        
    protected BaseQuery( TQuery request)
    {
        
        Request = request;
    }

    #endregion
}