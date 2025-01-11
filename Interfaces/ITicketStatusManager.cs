using Ticket.Enums;

namespace Ticket.Interfaces;
public interface ITicketStatusManager
{
    public bool CanTransition(TicketStatus currentStatus, TicketStatus newStatus);
}
