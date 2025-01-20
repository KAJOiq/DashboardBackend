using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ticket.Dtos.Tickets;
using Ticket.Interfaces;
using Ticket.Mappers;

namespace Ticket.Controllers;
[Route("api/ticket")]
[ApiController]
public class TicketController(ITicketRepository ticketRepository, IImageService imageService, IUserInformationFromToken userInformation, UserManager<IdentityUser> userManager) : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager = userManager;
    private readonly IImageService _imageService = imageService;
    private readonly ITicketRepository _ticketRepository = ticketRepository;
    private readonly IUserInformationFromToken _userInformation = userInformation;

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
        var userInfo = await _userInformation.GetUserIdFromDatabase();
        var user = await _userManager.FindByIdAsync(userInfo.UserId);

        var isCustomer = await _userManager.IsInRoleAsync(user, "Customer");
        if (isCustomer)
        {
            var managerId = await _userInformation.GetManagerId(userInfo.UserDepartment);
            var customerTicket = ticketRequestDto.TicketFormCustomerCreateDTO(imagePath!, userInfo.UserDepartment,userInfo.UserId,managerId);
            await _ticketRepository.CreateAsync(customerTicket);
        }

        var isManager = await _userManager.IsInRoleAsync(user, "Manager");
        if (isManager)
        {
            var managerTicket = ticketRequestDto.TicketFormManagerCreateDTO(imagePath!, userInfo.UserDepartment,userInfo.UserId);
            await _ticketRepository.CreateAsync(managerTicket);
        }
        var isEmployee = await _userManager.IsInRoleAsync(user, "Employee");
        if (isEmployee)
        {
            var managerId = await _userInformation.GetManagerId(userInfo.UserDepartment);
            var managerTicket = ticketRequestDto.TicketFormEmployeeCreateDTO(imagePath!, userInfo.UserDepartment,managerId);
            await _ticketRepository.CreateAsync(managerTicket);
        }

        return Ok("created success");
    }

    [HttpPatch]
    [Route("assign/{id:int}")]
    public async Task<IActionResult> Assign([FromRoute] int id, [FromForm] string userId)
    {
        var ticket = await _ticketRepository.Assign(id, userId);
        return Ok("assigned");
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
