using Ticket.Dtos.Tickets;

namespace Ticket.Interfaces;
public interface ITicketRepository
{
    Task<List<Models.Ticket>> GetByDepartmentId(int id ,TicketQueryDto ticketQueryDto);
    Task<Models.Ticket> UpdateStatus(int id, UpdateStatusDto dto);
    Task<Models.Ticket> UpdatePriority(int id, UpdatePriorityDto dto);
}
