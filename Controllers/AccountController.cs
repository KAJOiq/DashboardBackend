using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ticket.Dtos.Account;
using Ticket.Interfaces;
using Ticket.Models;

namespace Ticket.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController(UserManager<User> userManager, ITokenService tokenService) : ControllerBase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto register)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User
            {
                UserName = register.UserName,
                Email = register.Email
            };
            var createUser = await _userManager.CreateAsync(user, register.Password);
            if (createUser.Succeeded)
            {
                var roleResult = await _userManager.AddToRoleAsync(user, "User");
                if (roleResult.Succeeded)
                {
                    return Ok("account created successfully");
                }
                else
                {
                    return StatusCode(500, roleResult.Errors);
                }
            }
            else
            {
                return StatusCode(500, createUser.Errors);
            }
        }
        catch (Exception e)
        {
            return StatusCode(500, e);
        }
    }

    [HttpPost]
    public async Task<ActionResult> CreateSome()
    {
        return Ok();
    }
}
