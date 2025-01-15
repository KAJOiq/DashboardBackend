namespace Ticket.Models;
public class SubProblem
{
    public int Id { get; set; }
    public int MainProblemId { get; set; }
    public MainProblem MainProblem { get; set; } = null!;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
