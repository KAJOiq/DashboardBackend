using Microsoft.AspNetCore.Mvc;
using Ticket.Dtos.MainProblemDto;
using Ticket.Interfaces;
using Ticket.Mappers;

namespace Ticket.Controllers;
[Route("api/main-problem")]
[ApiController]
public class MainProblemController(IMainProblemRepository mainProblemRepository) : ControllerBase
{
    private readonly IMainProblemRepository _mainProblemRepository = mainProblemRepository;
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] MainProblemQueryDto query)
    {
        var problem = await _mainProblemRepository.GetAllAsync(query);
        return Ok(problem);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var problem = await _mainProblemRepository.GetByIdAsync(id);
        return Ok(problem.ToMainProblemDto());
    }
    
    [HttpGet("department/{departmentId:int}")]
    public async Task<IActionResult> GetByDepartmentId([FromRoute] int departmentId)
    {
        var problem = await _mainProblemRepository.GetByDepartmentId(departmentId);
        if (problem == null)
        {
            return NotFound();
        }
        var departmentDto = problem.Select(s => s.ToMainProblemDto());
        return Ok(departmentDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MainProblemRequestDto requestDto)
    {
        var problemModel = requestDto.ToMainProblemFormCreateDTO();
        await _mainProblemRepository.CreateAsync(problemModel);
        return CreatedAtAction(nameof(GetById), new { id = problemModel.Id }, problemModel.ToMainProblemDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateMainProblemRequestDto updateDto)
    {
        var problemModel = await _mainProblemRepository.UpdateAsync(id, updateDto);
        return Ok(problemModel.ToMainProblemDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _mainProblemRepository.DeleteAsync(id);
        return Ok("deleted");
    }
}
