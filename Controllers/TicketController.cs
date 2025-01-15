using Microsoft.AspNetCore.Mvc;
using Ticket.Dtos.Tickets;
using Ticket.Interfaces;
using Ticket.Mappers;

namespace Ticket.Controllers;
[Route("api/ticket")]
[ApiController]
public class TicketController(ITicketRepository ticketRepository, IImageService imageService) : ControllerBase
{
    private readonly IImageService _imageService = imageService;
    private readonly ITicketRepository _ticketRepository = ticketRepository;

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetByDepartmentId([FromRoute] int id, [FromQuery] TicketQueryDto query)
    {
        var tickets = await _ticketRepository.GetByDepartmentId(id, query);
        return Ok(tickets);
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromForm] TicketRequestDto ticketRequestDto, IFormFile image)
    {
        string? imagePath = null;

        if (image != null)
        {
            imagePath = await _imageService.UploadImageAsync(image);
        }
        var departmentModel = ticketRequestDto.TicketFormCreateDTO(imagePath!);
        await _ticketRepository.CreateAsync(departmentModel);
        return Ok("created success");
    }

    [HttpPatch]
    [Route("status/{id:int}")]
    public async Task<IActionResult> UpdateStatus([FromRoute] int id, [FromBody] UpdateStatusDto dto)
    {
        var ticket = await _ticketRepository.UpdateStatus(id, dto);
        return Ok("updated");
    }

    [HttpPatch]
    [Route("priority/{id:int}")]
    public async Task<IActionResult> UpdatePriority([FromRoute] int id, [FromBody] UpdatePriorityDto dto)
    {
        var ticket = await _ticketRepository.UpdatePriority(id, dto);
        return Ok("updated");
    }
}
