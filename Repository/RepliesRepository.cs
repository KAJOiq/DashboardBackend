using Microsoft.EntityFrameworkCore;
using Ticket.Contracts;
using Ticket.Data;
using Ticket.Dtos.RepliesDto;
using Ticket.Interfaces;
using Ticket.Models;

namespace Ticket.Repository;
public class RepliesRepository(ApplicationDBContext context) : IRepliesRepository
{
    private readonly ApplicationDBContext _context = context;
    public async Task<Replies> CreateAsync(Replies repliesModel)
    {
        await _context.Replies.AddAsync(repliesModel);
        await _context.SaveChangesAsync();
        return repliesModel;
    }

    public async Task<Replies> DeleteAsync(int id)
    {
        var replyModel = await _context.Replies.FirstOrDefaultAsync(x => x.Id == id);
        if (replyModel == null)
        {
            throw new KeyNotFoundException($"reply ID {id} not found.");
        }
        _context.Replies.Remove(replyModel);
        await _context.SaveChangesAsync();
        return replyModel;
    }

    public async Task<PaginatedResponse<Replies>> GetAllAsync(RepliesQueryDto dto)
    {
        IQueryable<Replies> query = _context.Replies;

        return await PaginatedResponse<Replies>.CreateAsync(query, dto.CurrentPage, dto.PageSize);
    }

    public async Task<Replies> GetByIdAsync(int id)
    {
        var reply = await _context.Replies.FirstOrDefaultAsync(i => i.Id == id);
        if (reply == null)
        {
            throw new KeyNotFoundException($"reply ID {id} not found.");
        }
        return reply;
    }

    public async Task<PaginatedResponse<Replies>> GetByTicketId(int id, RepliesQueryDto dto)
    {
        var query = _context.Replies
           .Where(d => d.TicketId == id);

        return await PaginatedResponse<Replies>.CreateAsync(
            query,
            dto.CurrentPage,
            dto.PageSize
        );
    }

    public async Task<Replies> UpdateAsync(int id, UpdateRepliesDto dto)
    {
        var existingReply = await _context.Replies.FirstOrDefaultAsync(x => x.Id == id);
        if (existingReply == null)
        {
            throw new KeyNotFoundException($"reply ID {id} not found.");
        }
        existingReply.Note = dto.Note;
        existingReply.Description = dto.Description;
        existingReply.Photo_url = dto.Photo_url;

        await _context.SaveChangesAsync();
        return existingReply;
    }
}
