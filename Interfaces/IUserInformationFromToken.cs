using Ticket.Dtos.Account;

namespace Ticket.Interfaces;
public interface IUserInformationFromToken
{
    Task<UserInfoDto> GetUserIdFromDatabase();
    Task<string> GetManagerId(int departmentId);
}
