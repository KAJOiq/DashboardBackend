namespace Ticket.Dtos.MainProblemDto;
public record GetMainProblemDto(
    int Id,
    int DepartmentId,
    string Name,
    string Description
);
