using Ticket.Models;

namespace Ticket.Interfaces;
public interface IUserDepartmentRepository
{
    Task<UserDepartment> CreateAsync(UserDepartment userDepartment);
    Task<UserDepartment> DeleteAsync(int id);
}
