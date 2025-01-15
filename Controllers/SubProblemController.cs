using Microsoft.AspNetCore.Mvc;
using Ticket.Dtos.SubProblemDto;
using Ticket.Interfaces;
using Ticket.Mappers;

namespace Ticket.Controllers;
[Route("api/sub-problem")]
[ApiController]
public class SubProblemController(ISubProblemRepository subProblemRepository) : ControllerBase
{
    private readonly ISubProblemRepository _subProblemRepository = subProblemRepository;
 [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] SubProblemQueryDto query)
    {
        var problem = await _subProblemRepository.GetAllAsync(query);
        return Ok(problem);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var problem = await _subProblemRepository.GetByIdAsync(id);
        return Ok(problem.ToSubProblemDto());
    }
    
    [HttpGet("department/{departmentId:int}")]
    public async Task<IActionResult> GetByDepartmentId([FromRoute] int departmentId)
    {
        var employees = await _subProblemRepository.GetByMainProblemId(departmentId);
        if (employees == null)
        {
            return NotFound();
        }
        var departmentDto = employees.Select(s => s.ToSubProblemDto());
        return Ok(departmentDto);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] SubProblemRequestDto requestDto)
    {
        var problemModel = requestDto.ToSubProblemFormCreateDTO();
        await _subProblemRepository.CreateAsync(problemModel);
        return CreatedAtAction(nameof(GetById), new { id = problemModel.Id }, problemModel.ToSubProblemDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateSubProblemRequestDto updateDto)
    {
        var problemModel = await _subProblemRepository.UpdateAsync(id, updateDto);
        return Ok(problemModel.ToSubProblemDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _subProblemRepository.DeleteAsync(id);
        return Ok("deleted");
    }
}
