using Ticket.Dtos.Account;
using Ticket.Models;

namespace Ticket.Mappers;
public static class AccountMapper
{
    public static NewUserDto ToUserDto(this User userModel,string token)
    {
        return new NewUserDto(
            userModel.Email??"",
            userModel.UserName??"",
            token
        );
    }
    public static User ToUserFormCreateDTO(this RegisterDto register, string imagePath)
    {
        return new User
        {
            SecondPhone = register.SecondPhone,
            Position = register.SecondPhone,
            UserName = register.UserName,
            Email = register.Email,
            Photo_url = imagePath,
            Sex = register.Sex,
            DOB = register.DOB,
            Address = register.Address,
            EmploymentDate = register.EmploymentDate
        };
    }

}
