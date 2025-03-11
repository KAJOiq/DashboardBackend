using Ticket.Dtos.Account;

namespace DashboardBackend.Interfaces;
public interface IUserInformationFromToken
{
    Task<UserInfoDto> GetUserIdFromDatabase();
}
