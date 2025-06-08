
using Shared.Models.CRM;
using Microsoft.AspNetCore.Mvc;
using MotionServiceProxies.Command.Query;
using Refit;

namespace MotionServiceProxies.HttpProxy;

public interface ITicketServiceProxy
{
    
    [Get("/crm/main/v1/ticket")]
    Task<ApiResponse<TicketDto?>> GetV1TicketAsync(
        ReadTicketQueryCommand request,
        CancellationToken cancellationToken = default (CancellationToken));

    [Get("/crm/main/v1/ticket/events")]
    Task<ApiResponse<ReadEventsQueryCommandResponse>> GetTicketEventsAsync(
        ReadEventsQueryCommand request,
        CancellationToken cancellationToken = default (CancellationToken));

    [Get("/crm/main/v1/ticket/files")]
    Task<ApiResponse<List<FileListItem>>> GetTicketFilesAsync(
        ReadAttachmentsQueryCommand request,
        CancellationToken cancellationToken = default (CancellationToken));

    [Get("/crm/main/v1/ticket/file")]
    Task<ApiResponse<FileResult>> GetTicketFileStreamAsync(
        ReadAttachmentQuery request,
        CancellationToken cancellationToken = default (CancellationToken));
    
    [Get("/crm/main/v1/tickets")]
    Task<ApiResponse<TicketQueryResponse>> GetVTicketsAsync(
        [FromQuery] ReadTicketsQuery request);
}