using MediatR;

namespace MotionServiceProxies.Command.Query;

public class ReadAttachmentsQueryCommand : IRequest<List<FileListItem>>, IBaseRequest
{
    public int TenantId { get; set; }

    public long TicketId { get; set; }
}