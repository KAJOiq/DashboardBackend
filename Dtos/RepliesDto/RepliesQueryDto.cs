namespace Ticket.Dtos.RepliesDto;
public class RepliesQueryDto
{
    public int CurrentPage { set; get; } = 1;
    public int PageSize { set; get; } = 10;
}
