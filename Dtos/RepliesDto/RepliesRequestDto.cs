namespace Ticket.Dtos.RepliesDto;
public record RepliesRequestDto(
    int TicketId,
    string Description,
    string Note,
    string AssignorId,
    string AssignId
);
