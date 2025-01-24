using projects.Dtos.Account;

namespace projects.Interfaces;
public interface IUserInformationFromToken
{
    Task<UserInfoDto> GetUserIdFromDatabase();
    Task<string> GetManagerId(int departmentId);
}
