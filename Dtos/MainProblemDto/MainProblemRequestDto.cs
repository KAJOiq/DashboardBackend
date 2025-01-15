namespace Ticket.Dtos.MainProblemDto;
public record MainProblemRequestDto(
    int DepartmentId,
    string Name,
    string Description
);
