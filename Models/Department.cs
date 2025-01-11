namespace Ticket.Models;
public class Department
{
    public int Id { get; set; }
    public string DepartmentName {set; get;} = string.Empty;
    public List<UserDepartment> UserDepartments {set; get;} = [];
}
