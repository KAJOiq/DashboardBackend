namespace Ticket.Models;
public class MainProblem
{
    public int Id{ get; set; }
    public int DepartmentId {set; get;}
    public Department? Department {set; get;}
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<SubProblem> SubProblems { set; get; } = [];
}
