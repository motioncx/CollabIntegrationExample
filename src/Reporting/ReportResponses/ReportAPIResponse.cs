using System.Collections;
using CollabIntegrationExample.WebApp.Command.Reports.Domain;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CollabIntegrationExample.WebApp.Command.Reports.ReportResponses;

public class ReportAPIResponse<T>
{
    private readonly ResponseCodes _resultCode;

    #region Properties

    [JsonIgnore]
    public ResponseCodes ResultCode => _resultCode;

    [JsonProperty("resultCode")]
    public int NumericResultCode => (int)_resultCode;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? ErrorMessage { get; }

    public string ResultCodeName => ResultCode.ToString();

    public T? Data { get; }

    public string DataType => typeof(T) switch
    {
        var t when typeof(JObject).IsAssignableFrom(t) => t.Name,
        var t when typeof(IEnumerable).IsAssignableFrom(t) => t.GetGenericArguments()[0].Name,
        var t => t.Name
    };

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public Pagination? Paging { get; }
    public bool HasPaging => Paging != null;

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public long? TotalResults { get; set; }

    #endregion

    #region Constructors/Destructors
    internal ReportAPIResponse() {}
    public ReportAPIResponse(T data)
    {
        _resultCode = ResponseCodes.Success;
        Data = data;
        if ((Data is ReportV3APIResponseError) && (ResultCode == ResponseCodes.Success))
            _resultCode = ResponseCodes.Error;
    }

    [System.Text.Json.Serialization.JsonConstructor]
    public ReportAPIResponse(ResponseCodes resultCode, T? data = default, Pagination? pageDetails = null, string? errorMessage = null)
    {
        Data = data;
        _resultCode = resultCode;
        Paging = pageDetails;
        ErrorMessage = errorMessage;
    }

    #endregion

    #region Static Helper Methods
    public static ReportAPIResponse<T> Success(T data, Pagination? pageDetails = null)
    {
        return new ReportAPIResponse<T>(ResponseCodes.Success, data, pageDetails);
    }

    public static ReportAPIResponse<T> SuccessOrNotFound(T data, PageDetails? pageDetails = null)
    {
        if (data is null)
        {
            return NotFound();
        }
        return Success(data);
    }

    public static ReportAPIResponse<T> NotSuccessful(ResponseCodes code)
    {
        return new ReportAPIResponse<T>(code, errorMessage: code.GetDescription());
    }

    public static ReportAPIResponse<T> NotFound()
    {
        return NotSuccessful(ResponseCodes.NotFound);
    }

    public static ReportAPIResponse<T> AuthorizationFailed()
    {
        return NotSuccessful(ResponseCodes.AuthorizationFailed);
    }

    public static ReportAPIResponse<T> Error()
    {
        return NotSuccessful(ResponseCodes.Error);
    }

    #endregion
}