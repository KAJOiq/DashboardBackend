using Microsoft.EntityFrameworkCore;
using Ticket.Contracts;
using Ticket.Data;
using Ticket.Dtos.Department;
using Ticket.Interfaces;
using Ticket.Models;

namespace Ticket.Repository;
public class DepartmentRepository(ApplicationDBContext context) : IDepartmentRepository
{
    private readonly ApplicationDBContext _context = context;
    public async Task<Department> CreateAsync(Department departmentModel)
    {
        await _context.Department.AddAsync(departmentModel);
        await _context.SaveChangesAsync();
        return departmentModel;
    }

    public async Task<Department> DeleteAsync(int id)
    {
        var departmentModel = await _context.Department.FirstOrDefaultAsync(x => x.Id == id);
        if (departmentModel == null)
        {
            throw new KeyNotFoundException($"Department ID {id} not found.");
        }
        _context.Department.Remove(departmentModel);
        await _context.SaveChangesAsync();
        return departmentModel;
    }

    public async Task<PaginatedResponse<Department>> GetAllAsync(DepartmentQueryDto departmentQueryDto)
    {
        IQueryable<Department> query = _context.Department;

        return await PaginatedResponse<Department>.CreateAsync(query, departmentQueryDto.CurrentPage, departmentQueryDto.PageSize);
    }

    public async Task<Department> GetByIdAsync(int id)
    {
        var department = await _context.Department.FirstOrDefaultAsync(i => i.Id == id);
        if (department == null)
        {
            throw new KeyNotFoundException($"Department ID {id} not found.");
        }
        return department;
    }

    public async Task<Department> UpdateAsync(int id, UpdateDepartmentRequestDto departmentDto)
    {
        var existingDepartment = await _context.Department.FirstOrDefaultAsync(x => x.Id == id);
        if (existingDepartment == null)
        {
            throw new KeyNotFoundException($"Department ID {id} not found.");
        }
        existingDepartment.DepartmentName = departmentDto.DepartmentName;

        await _context.SaveChangesAsync();
        return existingDepartment;
    }
}
