using Microsoft.AspNetCore.Mvc;
using Ticket.Dtos.UserDepartment;
using Ticket.Interfaces;
using Ticket.Mappers;

namespace Ticket.Controllers;
[Route("api/user-department")]
[ApiController]
public class UserDepartmentController(IUserDepartmentRepository userDepartment) : ControllerBase
{
    private readonly IUserDepartmentRepository _userDepartment = userDepartment;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserDepartmentRequestDto userDepartmentRequestDto)
    {
        var departmentModel = userDepartmentRequestDto.ToUserDepartmentFormCreateDTO();
        await _userDepartment.CreateAsync(departmentModel);
        return Ok("created success");
    }
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _userDepartment.DeleteAsync(id);
        return Ok("deleted");
    }
}
