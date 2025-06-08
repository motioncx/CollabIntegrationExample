namespace CollabIntegrationExample.WebApp.Command.Reports.ReportResponses;

public static class ReportV3APIResponseHelpers
{
    #region Static Extension Methods
        
    public static ReportAPIResponse<ReportV3APIResponseError> Error(string ErrorMessage)
    {
        return new ReportAPIResponse<ReportV3APIResponseError>(ResponseCodes.Error, new ReportV3APIResponseError()
        {
            Error = ErrorMessage
        });
    }

    public static ReportAPIResponse<ReportV3APIResponseError> NotFound(string message = null)
    {
        return new ReportAPIResponse<ReportV3APIResponseError>(ResponseCodes.NotFound, new ReportV3APIResponseError()
        {
            ErrorType = "NotFound",
            Error = message == null ? "404 Not Found" : message
        }); ; 
    }

    public static ReportAPIResponse<ReportV3APIResponseError> InvalidRequest(string message = null)
    {
        return new ReportAPIResponse<ReportV3APIResponseError>(ResponseCodes.InvalidRequest, new ReportV3APIResponseError()
        {
            ErrorType = "InvalidRequest",
            Error = message == null ? "Invalid Request" : message
        });
    }

    public static ReportAPIResponse<ReportV3APIResponseError> AuthorizationFailed(string message = null)
    {
        return new ReportAPIResponse<ReportV3APIResponseError>(ResponseCodes.AuthorizationFailed, new ReportV3APIResponseError()
        {
            ErrorType = "AuthorizationFailed",
            Error = message == null ? "Authorization Failed" : message
        });
    }
    #endregion HELPERS
}