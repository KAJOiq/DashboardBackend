using Microsoft.EntityFrameworkCore;
using Ticket.Data;
using Ticket.Interfaces;
using Ticket.Models;

namespace Ticket.Repository;
public class UserDepartmentRepository(ApplicationDBContext context) : IUserDepartmentRepository
{
    private readonly ApplicationDBContext _context = context;
    public async Task<UserDepartment> CreateAsync(UserDepartment userDepartment)
    {
        await _context.UserDepartment.AddAsync(userDepartment);
        await _context.SaveChangesAsync();
        return userDepartment;
    }

    public async Task<UserDepartment> DeleteAsync(int id)
    {
        var userDepartmentModel = await _context.UserDepartment.FirstOrDefaultAsync(x => x.Id == id);
        if (userDepartmentModel == null)
        {
            throw new KeyNotFoundException($"UserDepartment ID {id} not found.");
        }
        _context.UserDepartment.Remove(userDepartmentModel);
        await _context.SaveChangesAsync();
        return userDepartmentModel;
    }
}
