using Microsoft.AspNetCore.Mvc;
using Ticket.Dtos.Department;
using Ticket.Interfaces;
using Ticket.Mappers;

namespace Ticket.Controllers;
[Route("api/department")]
[ApiController]
public class DepartmentController(IDepartmentRepository departmentRepository):ControllerBase
{
private readonly IDepartmentRepository _departmentRepository = departmentRepository;
   [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] DepartmentQueryDto query)
    {
        var department = await _departmentRepository.GetAllAsync(query);
        return Ok(department);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var department = await _departmentRepository.GetByIdAsync(id);
        return Ok(department.ToDepartmentDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DepartmentRequestDto departmentRequestDto)
    {
        var departmentModel = departmentRequestDto.ToDepartmentFormCreateDTO();
        await _departmentRepository.CreateAsync(departmentModel);
        return CreatedAtAction(nameof(GetById), new { id = departmentModel.Id }, departmentModel.ToDepartmentDto());
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateDepartmentRequestDto updateDto)
    {
        var departmentModel = await _departmentRepository.UpdateAsync(id, updateDto);
        return Ok(departmentModel.ToDepartmentDto());
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _departmentRepository.DeleteAsync(id);
        return Ok("deleted");
    }
}
