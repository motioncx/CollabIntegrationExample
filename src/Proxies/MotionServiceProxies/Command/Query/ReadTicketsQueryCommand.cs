using Shared.Models.CRM;
using MediatR;

namespace MotionServiceProxies.Command.Query;

public class ReadTicketsQuery : ReadTicketsModel, IRequest<TicketQueryResponse>
{
}
