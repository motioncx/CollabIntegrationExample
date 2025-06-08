using Newtonsoft.Json.Linq;

namespace CollabIntegrationExample.WebApp.Command.Reports.Interfaces;

public interface IFilteredQuery
{
    public JObject Filter { get; }
}