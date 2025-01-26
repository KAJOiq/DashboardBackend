namespace projects.Models;
public class Students
{
    public int Id {set; get;}
    public int ProjectId { set; get; }
    public Project Project {set; get;} = null!;
    public string StudentId { get; set; } = string.Empty;
    public User? Student { set; get; } = null!;
}
