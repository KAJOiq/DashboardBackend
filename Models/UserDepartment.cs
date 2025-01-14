namespace Ticket.Models;
public class UserDepartment
{
    public int Id;
    public string UserId {set; get;} 
    public int DepartmentId {set; get;} 
    public User? User {set; get;} = null!;
    public Department? Department {set; get;}
}