using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ticket.Dtos.Account;
using Ticket.Interfaces;
using Ticket.Models;
using System.Security.Claims;
using Ticket.Mappers;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;

namespace Ticket.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController(UserManager<User> userManager, ITokenService tokenService, SignInManager<User> signInManager, IAccountManager accountManager, IImageService imageService) : ControllerBase
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly ITokenService _tokenService = tokenService;
    private readonly SignInManager<User> _signInManager = signInManager;
    private readonly IAccountManager _accountManager = accountManager;
    private readonly IImageService _imageService = imageService;

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterDto register, IFormFile image)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            string? imagePath = null;

            if (image != null)
            {
                imagePath = await _imageService.UploadImageAsync(image);
            }
            var user = register.ToUserFormCreateDTO(imagePath!);
            var createUser = await _userManager.CreateAsync(user, register.Password ?? "");
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
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());
        if (user == null) return Unauthorized("Invalid user name");
        var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);
        if (!result.Succeeded) return Unauthorized("UserName not found or password incorrect");
        return Ok(user.ToUserDto(_tokenService.CreateToken(user)));
    }
    [HttpGet("by-position")]
    public async Task<IActionResult> GetUsersByPosition([FromQuery] string position)
    {
        var users = await _accountManager.GetUsers(position);
        return Ok(users);
    }

    [HttpPost]
    [Route("create-role")]
    public async Task<IActionResult> CreateRole([FromBody] CreateRoleDto dto)
    {
        try
        {
            var result = await _accountManager.CreateRoleAsync(dto);
            if (result.Succeeded)
            {
                return Ok("Role created successfully.");
            }

            return BadRequest("Failed to create the role.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet]
    [Route("exists/{roleName}")]
    public async Task<IActionResult> RoleExists(string roleName)
    {
        var exists = await _accountManager.RoleExistsAsync(roleName);
        return Ok(new { RoleName = roleName, Exists = exists });
    }

    [HttpGet]
    [Route("find")]
    public async Task<IActionResult> FindRoleByName([FromQuery] GetRoleDto dto)
    {
        var role = await _accountManager.FindRoleByNameAsync(dto);
        if (role == null)
        {
            return NotFound("Role not found.");
        }

        return Ok(role);
    }

    [HttpPost]
    [Route("add-claim")]
    public async Task<IActionResult> AddClaimToRole([FromBody] AddClaimToRoleDto dto)
    {
        try
        {
            var claim = new Claim(dto.ClaimType, dto.ClaimValue);
            var result = await _accountManager.AddClaimToRoleAsync(new CreateRoleDto { RoleName = dto.RoleName }, claim);

            if (result.Succeeded)
            {
                return Ok("Claim added to role successfully.");
            }

            return BadRequest("Failed to add claim to role.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [Authorize]

    [HttpGet("user-id")]
    public async Task<IActionResult> GetUserIdFromDatabase()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) 
            ?? User.FindFirstValue("sub") 
            ?? User.FindFirstValue("uid")
            ?? User.Claims.FirstOrDefault(c => c.Type.ToLower().Contains("userid"))?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            // If we still can't find the user ID, let's log all claims for debugging
            var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
            return BadRequest(new { Message = "userId is missing in the token.", Claims = claims });
        }

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
        {
            return NotFound($"User not found for ID: {userId}");
        }

        return Ok(new { UserId = user.Id });
    }

}
