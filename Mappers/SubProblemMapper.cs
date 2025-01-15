using Ticket.Dtos.SubProblemDto;
using Ticket.Models;

namespace Ticket.Mappers;
public static class SubProblemMapper
{
    public static GetSubProblemDto ToSubProblemDto(this SubProblem problemModel)
    {
        return new GetSubProblemDto(
            problemModel.Id,
            problemModel.MainProblemId,
            problemModel.Name,
            problemModel.Description
        );
    }
    public static SubProblem ToSubProblemFormCreateDTO(this SubProblemRequestDto problemDto)
    {
        return new SubProblem
        {
            MainProblemId = problemDto.MainProblemId,
            Name = problemDto.Name,
            Description = problemDto.Name
        };
    }
}
