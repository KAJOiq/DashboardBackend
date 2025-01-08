using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ticket.Dtos.Account;
using Ticket.Interfaces;
using Ticket.Models;

namespace Ticket.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController(UserManager<User> userManager, ITokenService tokenService,SignInManager<User> signInManager) : ControllerBase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly SignInManager<User> _signInManager = signInManager;
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
                SecondPhone =  "",
                Position = "",
                UserName = register.UserName,
                Email = register.Email,
            };
            var createUser = await _userManager.CreateAsync(user, register.Password ??"");
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
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto){
        if(!ModelState.IsValid){
            return BadRequest(ModelState);
        }
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());
        if(user == null) return Unauthorized("Invalid user name");
        var result = await _signInManager.CheckPasswordSignInAsync(user ,loginDto.Password,false );
        if(!result.Succeeded) return Unauthorized("UserName not found or password incorrect");
        return Ok(new NewUserDto{
            Username = user.UserName,
            Email = user.Email,
            Token = _tokenService.CreateToken(user)
        });
    }
}
