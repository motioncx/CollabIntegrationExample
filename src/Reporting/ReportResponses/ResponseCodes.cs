using System.ComponentModel;
using System.Linq;

namespace CollabIntegrationExample.WebApp.Command.Reports.ReportResponses;

public enum ResponseCodes
{
    [Description("Success")]
    Success = 0,
        
    [Description("The requested item was Not found.")] 
    NotFound = 1,
        
    [Description("Authorization failed.")]
    AuthorizationFailed = 2,
        
    [Description("Unexpected error")]
    Error = 3,
        
    [Description("The request was malformed or invalid.")]
    InvalidRequest = 4,
}

public static class ResponseCodeExtensions
{
    public static string GetDescription(this ResponseCodes self)
    {
        var descriptionAttribute = self.GetType()
                .GetField(self.ToString())
                ?.GetCustomAttributes(false)
                .SingleOrDefault(attr => attr.GetType() == typeof(DescriptionAttribute)) 
            as DescriptionAttribute;
        
        // return description
        return descriptionAttribute?.Description ?? "";
    }
}