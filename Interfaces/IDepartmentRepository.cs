using Ticket.Dtos.Department;
using Ticket.Models;

namespace Ticket.Interfaces;
public interface IDepartmentRepository
{
    Task<List<Department>> GetAllAsync(DepartmentQueryDto departmentQueryDto);
    Task<Department> GetByIdAsync(int id);
    Task<List<Department>> GetByCompanyId(int id);
    Task<Department> CreateAsync(DepartmentRequestDto departmentModel);
    Task<Department> UpdateAsync(int id, UpdateDepartmentRequestDto departmentDto);
    Task<Department> DeleteAsync(int id);
}

