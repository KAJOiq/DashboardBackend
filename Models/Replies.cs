namespace Ticket.Models;
public class Replies
{
    public int Id {set; get;}
    public int TicketId { set; get; }
    public Ticket Ticket {set; get;} = null!;
    public string Description { get; set; } = string.Empty;
    public string Note { get; set; } = string.Empty;
    public string Photo_url { get; set; } = string.Empty;
    public string AssignorId { get; set; } = string.Empty;
    public User? Assignor { set; get; } = null!;
    public string AssignId { get; set; } = string.Empty;
    public User? Assign { set; get; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
