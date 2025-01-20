using Ticket.Contracts;
using Ticket.Data;
using Ticket.Dtos.Tickets;
using Ticket.Enums;
using Ticket.Interfaces;

namespace Ticket.Repository;
public class TicketRepository(ApplicationDBContext context, ITicketStatusManager ticketStatusManager,ITicketPriorityManager ticketPriorityManager) : ITicketRepository
{
    private readonly ITicketStatusManager _ticketStatusManager = ticketStatusManager;
    private readonly ApplicationDBContext _context = context;
    private readonly ITicketPriorityManager _ticketPriorityManager = ticketPriorityManager;

    public async Task<Models.Ticket> Assign(int ticketId ,string userId)
    {
        var ticket = await _context.Ticket.FindAsync(ticketId);
        if (ticket == null)
        {
            throw new KeyNotFoundException($"Ticket ID {ticketId} not found.");
        }
        ticket.AssignId = userId;
        await _context.SaveChangesAsync();

        return ticket;
    }

    public async Task<Models.Ticket> CreateAsync(Models.Ticket ticket)
    {
        await _context.Ticket.AddAsync(ticket);
        await _context.SaveChangesAsync();
        return ticket;
    }

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

    public async Task<Models.Ticket> UpdatePriority(int id, UpdatePriorityDto dto)
    {
        var ticket = await _context.Ticket.FindAsync(id);
        if (ticket == null)
        {
            throw new KeyNotFoundException($"Ticket ID {id} not found.");
        }

        var newPriority = (TicketPriority)Enum.Parse(typeof(TicketStatus), dto.NewPriority, true);
        if (!_ticketPriorityManager.CanTransition(ticket.Priority, newPriority))
        {
            throw new Exception("Invalid Priority transition");
        }

        ticket.Priority = newPriority;
        await _context.SaveChangesAsync();

        return ticket;
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
        
        if(newStatus == TicketStatus.Done){
            ticket.ClosedAt = DateTime.UtcNow;
        }

        ticket.Status = newStatus;
        await _context.SaveChangesAsync();

        return ticket;
    }
}
