using Microsoft.EntityFrameworkCore;
using Ticket.Contracts;
using Ticket.Data;
using Ticket.Dtos.Tickets;
using Ticket.Enums;
using Ticket.Interfaces;

namespace Ticket.Repository;
public class TicketRepository(ApplicationDBContext context, ITicketStatusManager ticketStatusManager) : ITicketRepository
{
    private readonly ITicketStatusManager _ticketStatusManager = ticketStatusManager;
    private readonly ApplicationDBContext _context = context;
    public async Task<List<Models.Ticket>> GetByDepartmentId(int id, TicketQueryDto ticketQueryDto)
    {
        var department = _context.Ticket
            .Where(d => d.DepartmentId == id);

        if (department == null)
        {
            throw new KeyNotFoundException($"Department ID {id} not found.");
        }

        var paginatedResponse = await PaginatedResponse<Models.Ticket>.CreateAsync(
           department,
           ticketQueryDto.CurrentPage,
           ticketQueryDto.PageSize
        );

        return paginatedResponse.Items.ToList();
    }

    public async Task<Models.Ticket> UpdateStatus(int id, UpdateStatusDto dto)
    {
        var ticket = await _context.Ticket.FindAsync(id);
        if (ticket == null)
        {
            throw new KeyNotFoundException($"Ticket ID {id} not found.");
        }

        var newStatus = (TicketStatus)Enum.Parse(typeof(TicketStatus), dto.NewStatus, true);
        if (!_ticketStatusManager.CanTransition(ticket.Status, newStatus))
        {
            throw new Exception("Invalid status transition");
        }

        ticket.Status = newStatus;
        await _context.SaveChangesAsync();

        return ticket;
    }
}
