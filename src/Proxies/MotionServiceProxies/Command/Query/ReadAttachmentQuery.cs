using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace MotionServiceProxies.Command.Query;

public class ReadAttachmentQuery : IRequest<FileResult>, IBaseRequest
{
    public int TenantId { get; set; }

    public long TicketId { get; set; }

    public string Filename { get; set; }
}