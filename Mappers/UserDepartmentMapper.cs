using Ticket.Dtos.UserDepartment;
using Ticket.Models;

namespace Ticket.Mappers;
public static class UserDepartmentMapper
{
    // public static GetDepartmentDto ToDepartmentDto(this Department companyModel)
    // {
    //     return new GetDepartmentDto(
    //         companyModel.Id,
    //         companyModel.DepartmentName);
    // }
    public static UserDepartment ToUserDepartmentFormCreateDTO(this UserDepartmentRequestDto userDepartmentDto)
    {
        return new UserDepartment
        {
            UserId = userDepartmentDto.UserId,
            DepartmentId = userDepartmentDto.DepartmentId
        };
    }

}
