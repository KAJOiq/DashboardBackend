using Microsoft.AspNetCore.Mvc;
using Ticket.Dtos.RepliesDto;
using Ticket.Interfaces;
using Ticket.Mappers;

namespace Ticket.Controllers;
[Route("api/replies")]
[ApiController]
public class RepliesController(IRepliesRepository repliesRepository) : ControllerBase
{
    private readonly IRepliesRepository _repliesRepository = repliesRepository;
    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] RepliesQueryDto query)
    {
        var problem = await _repliesRepository.GetAllAsync(query);
        return Ok(problem);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var replies = await _repliesRepository.GetByIdAsync(id);
        return Ok(replies.ToRepliesDto());
    }
    
    [HttpGet("ticket/{departmentId:int}")]
    public async Task<IActionResult> GetByTicketId([FromRoute] int departmentId , RepliesQueryDto dto)
    {
        var replies = await _repliesRepository.GetByTicketId(departmentId,dto);
        if (replies == null)
        {
            return NotFound();
        }
        return Ok(replies);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] RepliesRequestDto requestDto)
    {
        var repliesModel = requestDto.ToRepliesFormCreateDTO();
        await _repliesRepository.CreateAsync(repliesModel);
        return CreatedAtAction(nameof(GetById), new { id = repliesModel.Id }, repliesModel.ToRepliesDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRepliesDto updateDto)
    {
        var problemModel = await _repliesRepository.UpdateAsync(id, updateDto);
        return Ok(problemModel.ToRepliesDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _repliesRepository.DeleteAsync(id);
        return Ok("deleted");
    }
}