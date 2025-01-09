namespace Ticket.Dtos.Department;
public class DepartmentQueryDto
{
    public int CurrentPage { set; get; } = 1;
    public int PageSize { set; get; } = 10;
};
