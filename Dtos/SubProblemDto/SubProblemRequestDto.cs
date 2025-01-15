namespace Ticket.Dtos.SubProblemDto;
public record SubProblemRequestDto(
    int MainProblemId,
    string Name,
    string Description
);
