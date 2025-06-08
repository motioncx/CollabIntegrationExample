using CollabIntegrationExample.WebApp.Command.Reports.ReportResponses;
using MediatR;

namespace CollabIntegrationExample.WebApp.Command.Reports.Interfaces;

public interface IReportingRequest<T> : IRequest<ReportAPIResponse<T>>
{
}