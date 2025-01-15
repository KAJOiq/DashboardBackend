using Ticket.Contracts;
using Ticket.Dtos.MainProblemDto;
using Ticket.Models;

namespace Ticket.Interfaces;
public interface IMainProblemRepository
{
    Task<PaginatedResponse<MainProblem>> GetAllAsync(MainProblemQueryDto dto);
    Task<MainProblem> GetByIdAsync(int id);
    Task<List<MainProblem>> GetByDepartmentId(int id);
    Task<MainProblem> CreateAsync(MainProblem mainProblemModel);
    Task<MainProblem> UpdateAsync(int id, UpdateMainProblemRequestDto dto);
    Task<MainProblem> DeleteAsync(int id);
}
