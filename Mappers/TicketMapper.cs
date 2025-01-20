using Ticket.Dtos.Tickets;

namespace Ticket.Mappers;
public static class TicketMapper
{
    public static Models.Ticket TicketFormManagerCreateDTO(this TicketRequestDto dto, string imagePath, int userDepartment, string userId)
    {
        return new Models.Ticket
        {
            CustomerId = dto.CustomerId,
            DepartmentId = userDepartment,
            Title = dto.Title,
            Description = dto.Description,
            Note = dto.Note,
            Photo_url = imagePath,
            Status = dto.Status,
            Priority = dto.Priority,
            AssignorId = userId,
            AssignId = dto.AssignId,
            CreatedAt = DateTime.UtcNow
        };
    }
    public static Models.Ticket TicketFormEmployeeCreateDTO(this TicketRequestDto dto, string imagePath, int userDepartment, string managerId)
    {
        return new Models.Ticket
        {
            CustomerId = dto.CustomerId,
            DepartmentId = userDepartment,
            Title = dto.Title,
            Description = dto.Description,
            Note = dto.Note,
            Photo_url = imagePath,
            Status = dto.Status,
            Priority = dto.Priority,
            AssignorId = managerId,
            AssignId = dto.AssignId,
            CreatedAt = DateTime.UtcNow
        };
    }
    public static Models.Ticket TicketFormCustomerCreateDTO(this TicketRequestDto dto, string imagePath, int userDepartment, string userId, string managerId)
    {
        return new Models.Ticket
        {
            CustomerId = userId,
            DepartmentId = userDepartment,
            Title = dto.Title,
            Description = dto.Description,
            Note = dto.Note,
            Photo_url = imagePath,
            Status = dto.Status,
            Priority = dto.Priority,
            AssignorId = managerId,
            CreatedAt = DateTime.UtcNow
        };
    }
}
