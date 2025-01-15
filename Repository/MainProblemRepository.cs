using Microsoft.EntityFrameworkCore;
using Ticket.Contracts;
using Ticket.Data;
using Ticket.Dtos.MainProblemDto;
using Ticket.Interfaces;
using Ticket.Models;

namespace Ticket.Repository;
public class MainProblemRepository(ApplicationDBContext context) : IMainProblemRepository
{
    private readonly ApplicationDBContext _context = context;
    public async Task<MainProblem> CreateAsync(MainProblem mainProblemModel)
    {
        await _context.MainProblems.AddAsync(mainProblemModel);
        await _context.SaveChangesAsync();
        return mainProblemModel;
    }

    public async Task<MainProblem> DeleteAsync(int id)
    {
        var mainProblemModel = await _context.MainProblems.FirstOrDefaultAsync(x => x.Id == id);
        if (mainProblemModel == null)
        {
            throw new KeyNotFoundException($"main problem ID {id} not found.");
        }
        _context.MainProblems.Remove(mainProblemModel);
        await _context.SaveChangesAsync();
        return mainProblemModel;
    }

    public async Task<PaginatedResponse<MainProblem>> GetAllAsync(MainProblemQueryDto dto)
    {
        IQueryable<MainProblem> query = _context.MainProblems;

        return await PaginatedResponse<MainProblem>.CreateAsync(query, dto.CurrentPage, dto.PageSize);
    }

    public async Task<MainProblem> GetByIdAsync(int id)
    {
        var mainProblem = await _context.MainProblems.FirstOrDefaultAsync(i => i.Id == id);
        if (mainProblem == null)
        {
            throw new KeyNotFoundException($"main problem ID {id} not found.");
        }
        return mainProblem;
    }

    public async Task<List<MainProblem>> GetByDepartmentId(int id)
    {
        var problem = await _context.MainProblems
            .Where(d => d.DepartmentId == id)
            .ToListAsync();
        
        if (problem == null){
            throw new KeyNotFoundException($"Main problem ID {id} not found.");
        }
        return problem;
    }

    public async Task<MainProblem> UpdateAsync(int id, UpdateMainProblemRequestDto dto)
    {
        var existingMainProblem = await _context.MainProblems.FirstOrDefaultAsync(x => x.Id == id);
        if (existingMainProblem == null)
        {
            throw new KeyNotFoundException($"main problem ID {id} not found.");
        }
        existingMainProblem.Name = dto.Name;
        existingMainProblem.Description = dto.Description;


        await _context.SaveChangesAsync();
        return existingMainProblem;
    }
}
