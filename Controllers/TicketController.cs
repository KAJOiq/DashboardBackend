using Microsoft.AspNetCore.Mvc;
using Ticket.Dtos.Tickets;
using Ticket.Interfaces;

namespace Ticket.Controllers;
[Route("api/ticket")]
[ApiController]
public class TicketController(ITicketRepository ticketRepository) : ControllerBase
{
    private readonly ITicketRepository _ticketRepository = ticketRepository;

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetByDepartmentId([FromRoute] int id, [FromQuery] TicketQueryDto query)
    {
        var department = await _ticketRepository.GetByDepartmentId(id, query);
        return Ok(department);
    }
}
