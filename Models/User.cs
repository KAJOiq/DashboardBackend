using Microsoft.AspNetCore.Identity;

namespace Ticket.Models;
public class User : IdentityUser
{
    public string? SecondPhone { set; get; }
    public string Position { set; get; } = string.Empty;
    public string Sex { set; get; } = string.Empty;
    public DateTime DOB { get; set; }
    public string Photo_url { set; get; } = string.Empty;
    public string Address { set; get; } = string.Empty;
    public DateTime EmploymentDate { get; set; } = DateTime.UtcNow;
    public List<UserDepartment>? UserDepartments { set; get; }
    public List<Ticket>? UserTickets { set; get; }
}
