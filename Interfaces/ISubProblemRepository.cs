using Ticket.Contracts;
using Ticket.Dtos.SubProblemDto;
using Ticket.Models;

namespace Ticket.Interfaces;
public interface ISubProblemRepository
{
    Task<PaginatedResponse<SubProblem>> GetAllAsync(SubProblemQueryDto dto);
    Task<SubProblem> GetByIdAsync(int id);
    Task<List<SubProblem>> GetByMainProblemId(int id);
    Task<SubProblem> CreateAsync(SubProblem subProblemModel);
    Task<SubProblem> UpdateAsync(int id, UpdateSubProblemRequestDto dto);
    Task<SubProblem> DeleteAsync(int id);
}
