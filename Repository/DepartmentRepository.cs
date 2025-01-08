using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Data;
using Ticket.Dtos.Department;
using Ticket.Interfaces;
using Ticket.Models;

namespace Ticket.Repository
{
    public class DepartmentRepository(ApplicationDBContext context) : IDepartmentRepository
    {
        private readonly ApplicationDBContext _context = context;
        public Task<Department> CreateAsync(DepartmentRequestDto departmentModel)
        {
            throw new NotImplementedException();
        }

        public Task<Department> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Department>> GetAllAsync(DepartmentQueryDto departmentQueryDto)
        {
            throw new NotImplementedException();
        }

        public Task<List<Department>> GetByCompanyId(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Department> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Department> UpdateAsync(int id, UpdateDepartmentRequestDto departmentDto)
        {
            throw new NotImplementedException();
        }
    }
}