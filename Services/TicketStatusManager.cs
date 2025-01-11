using Ticket.Enums;
using Ticket.Interfaces;

namespace Ticket.Services;
public class TicketStatusManager : ITicketStatusManager
{
    private static readonly Dictionary<TicketStatus, List<TicketStatus>> ValidTransitions = new()
    {
        { TicketStatus.Pending, new List<TicketStatus> { TicketStatus.Working , TicketStatus.Done} },
        { TicketStatus.Working, new List<TicketStatus> { TicketStatus.Done } },
        { TicketStatus.Done, new List<TicketStatus>() }
    };

    public bool CanTransition(TicketStatus currentStatus, TicketStatus newStatus)
    {
        return ValidTransitions.ContainsKey(currentStatus) &&
               ValidTransitions[currentStatus].Contains(newStatus);
    }
}
