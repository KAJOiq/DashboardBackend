using Ticket.Enums;
using Ticket.Interfaces;

namespace Ticket.Services;
public class TicketPriorityManager : ITicketPriorityManager
{
    private static readonly Dictionary<TicketPriority, List<TicketPriority>> ValidTransitions = new()
    {
        { TicketPriority.Normal, new List<TicketPriority> { TicketPriority.Argent , TicketPriority.HighPriority} },
        { TicketPriority.HighPriority, new List<TicketPriority> { TicketPriority.Argent } },
        { TicketPriority.Argent, new List<TicketPriority>() }
    };

    public bool CanTransition(TicketPriority currentPriority, TicketPriority newPriority)
    {
        return ValidTransitions.ContainsKey(currentPriority) &&
               ValidTransitions[currentPriority].Contains(newPriority);
    }

}
