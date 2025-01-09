using System.ComponentModel.DataAnnotations;

namespace Ticket.Dtos.Account;
public class RegisterDto
{
    [Required]
    public string UserName {get; set;} = string.Empty;
    [Required]
    [EmailAddress]
    public string Email {get; set;} = string.Empty;
    [Required]
    public string PhoneNumber {get; set;} = string.Empty;
    [Required]
    public string Position {get; set;} = string.Empty;
    [Required]
    public string SecondPhone {get; set;} = string.Empty;
    [Required]
    public string Password {get; set;} = string.Empty;

}
