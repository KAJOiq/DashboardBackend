using Ticket.Dtos.MainProblemDto;
using Ticket.Models;

namespace Ticket.Mappers;
public static class MainProblemMapper
{
    public static GetMainProblemDto ToMainProblemDto(this MainProblem problemModel)
    {
        return new GetMainProblemDto(
            problemModel.Id,
            problemModel.DepartmentId,
            problemModel.Name,
            problemModel.Description
            );
    }
    public static MainProblem ToMainProblemFormCreateDTO(this MainProblemRequestDto problemDto)
    {
        return new MainProblem
        {
            DepartmentId = problemDto.DepartmentId,
            Name = problemDto.Name,
            Description = problemDto.Name
        };
    }

}
