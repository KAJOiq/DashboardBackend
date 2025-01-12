namespace Ticket.Models;
public class UserDepartment
{
    public int Id;
    public string UserId {set; get;} = string.Empty;
    public User? User {set; get;} = null!;
    public string DepartmentId {set; get;} = string.Empty;
    public Department? Department {set; get;}
}