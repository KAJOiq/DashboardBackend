using Microsoft.EntityFrameworkCore;
using Ticket.Contracts;
using Ticket.Data;
using Ticket.Dtos.SubProblemDto;
using Ticket.Interfaces;
using Ticket.Models;

namespace Ticket.Repository;
public class SubProblemRepository(ApplicationDBContext context) : ISubProblemRepository
{
    private readonly ApplicationDBContext _context = context;
    public async Task<SubProblem> CreateAsync(SubProblem subProblem)
    {
        await _context.SubProblems.AddAsync(subProblem);
        await _context.SaveChangesAsync();
        return subProblem;
    }

    public async Task<SubProblem> DeleteAsync(int id)
    {
        var subProblemModel = await _context.SubProblems.FirstOrDefaultAsync(x => x.Id == id);
        if (subProblemModel == null)
        {
            throw new KeyNotFoundException($"sub problem ID {id} not found.");
        }
        _context.SubProblems.Remove(subProblemModel);
        await _context.SaveChangesAsync();
        return subProblemModel;
    }

    public async Task<PaginatedResponse<SubProblem>> GetAllAsync(SubProblemQueryDto dto)
    {
        IQueryable<SubProblem> query = _context.SubProblems;

        return await PaginatedResponse<SubProblem>.CreateAsync(query, dto.CurrentPage, dto.PageSize);
    }

    public async Task<SubProblem> GetByIdAsync(int id)
    {
        var subProblem = await _context.SubProblems.FirstOrDefaultAsync(i => i.Id == id);
        if (subProblem == null)
        {
            throw new KeyNotFoundException($"sub problem ID {id} not found.");
        }
        return subProblem;
    }

    public async Task<List<SubProblem>> GetByMainProblemId(int id)
    {
        var problem = await _context.SubProblems
            .Where(d => d.MainProblemId == id)
            .ToListAsync();

        if (problem == null)
        {
            throw new KeyNotFoundException($"sub problem ID {id} not found.");
        }
        return problem;
    }

    public async Task<SubProblem> UpdateAsync(int id, UpdateSubProblemRequestDto dto)
    {
        var existingSubProblem = await _context.SubProblems.FirstOrDefaultAsync(x => x.Id == id);
        if (existingSubProblem == null)
        {
            throw new KeyNotFoundException($"sub problem ID {id} not found.");
        }
        existingSubProblem.Name = dto.Name;
        existingSubProblem.Description = dto.Description;

        await _context.SaveChangesAsync();
        return existingSubProblem;
    }
}
