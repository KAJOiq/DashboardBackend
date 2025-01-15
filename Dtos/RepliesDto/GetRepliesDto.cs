namespace Ticket.Dtos.RepliesDto;
public record GetRepliesDto(
 int Id,
 int TicketId,
 string Description,
 string Note,
 string Photo_url,
 string AssignorId,
 string AssignId,
 DateTime CreatedAt
);
