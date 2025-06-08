using Shared.Models;
using MediatR;

namespace MotionServiceProxies.Command.Query;

public class ReadEventsQueryCommand : 
    ReadListQuery,
    IRequest<ReadEventsQueryCommandResponse>,
    IBaseRequest
{
    public int TicketId { get; set; }
}