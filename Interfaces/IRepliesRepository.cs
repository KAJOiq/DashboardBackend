using Ticket.Contracts;
using Ticket.Dtos.RepliesDto;
using Ticket.Models;

namespace Ticket.Interfaces;
public interface IRepliesRepository
{
    Task<PaginatedResponse<Replies>> GetAllAsync(RepliesQueryDto dto);
    Task<Replies> GetByIdAsync(int id);
    Task<PaginatedResponse<Replies>> GetByTicketId(int id,RepliesQueryDto dto);
    Task<Replies> CreateAsync(Replies repliesModel);
    Task<Replies> UpdateAsync(int id, UpdateRepliesDto dto);
    Task<Replies> DeleteAsync(int id);

}
