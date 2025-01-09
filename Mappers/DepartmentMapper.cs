using Ticket.Dtos.Department;
using Ticket.Models;

namespace Ticket.Mappers;
public static class DepartmentMapper
{
    public static GetDepartmentDto ToDepartmentDto(this Department companyModel)
    {
        return new GetDepartmentDto(
            companyModel.Id,
            companyModel.DepartmentName);
    }
    public static Department ToDepartmentFormCreateDTO(this DepartmentRequestDto departmentDto)
    {
        return new Department
        {
            DepartmentName = departmentDto.DepartmentName
        };
    }
}
