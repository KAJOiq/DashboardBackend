using System.ComponentModel.DataAnnotations;
using Microsoft.Net.Http.Headers;

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
    [Required]
    public string Sex {get; set;} = string.Empty;
    [Required]
    public DateTime DOB {get; set;} 
    [Required]
    public string Address {get; set;} = string.Empty;
    [Required]
    public DateTime EmploymentDate {get; set;} 
    [Required]
    public string RoleId {get; set;} = string.Empty;
}
