using Ticket.Dtos.Tickets;

namespace Ticket.Mappers;
public static class TicketMapper
{
    public static Models.Ticket TicketFormCreateDTO(this TicketRequestDto dto)
    {
        return new Models.Ticket
        {
            CustomerId = dto.CustomerId,
            DepartmentId = dto.DepartmentId,
            Title = dto.Title,
            Description = dto.Description,
            Note = dto.Note,
            Photo_url = dto.PhotoUrl,
            Status = dto.Status,
            Priority = dto.Priority,
            AssignorId = dto.AssignorId,
            AssignId = dto.AssignId,
            CreatedAt = DateTime.UtcNow
        };
    }
}
