using Ticket.Contracts;
using Ticket.Dtos.Department;
using Ticket.Models;

namespace Ticket.Interfaces;
public interface IDepartmentRepository
{
    Task<PaginatedResponse<Department>> GetAllAsync(DepartmentQueryDto departmentQueryDto);
    Task<Department> GetByIdAsync(int id);
    Task<Department> CreateAsync(Department departmentModel);
    Task<Department> UpdateAsync(int id, UpdateDepartmentRequestDto departmentDto);
    Task<Department> DeleteAsync(int id);
}

