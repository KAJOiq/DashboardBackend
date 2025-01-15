namespace Ticket.Dtos.SubProblemDto;
public record GetSubProblemDto(
    int Id,
    int MainProblemId,
    string Name,
    string Description
);
