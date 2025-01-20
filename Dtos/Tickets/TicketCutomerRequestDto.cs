using Ticket.Enums;

namespace Ticket.Dtos.Tickets;
public class TicketCutomerRequestDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public TicketStatus Status { get; set; } = TicketStatus.Pending;
    public TicketPriority Priority { get; set; } = TicketPriority.Normal;
}
