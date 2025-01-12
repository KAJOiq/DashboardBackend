using Ticket.Enums;

namespace Ticket.Interfaces;
public interface ITicketPriorityManager
{
    public bool CanTransition(TicketPriority currentStatus, TicketPriority newStatus);
}
