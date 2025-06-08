using Shared.Models;
using Shared.Models.CRM;
using MediatR;

namespace MotionServiceProxies.Command.Query;

public class ReadTicketQueryCommand : ITenantable, IRequest<TicketDto>, IBaseRequest
{
    public int TenantId { get; set; }

    public long TicketId { get; set; }

    public bool Refresh { get; set; }
}