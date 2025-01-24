using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Ticket.Data;
using projects.Dtos.Account;
using projects.Interfaces;
using projects.Models;

namespace Project.Services;
public class UserInformationFromToken(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor, ApplicationDBContext context) : IUserInformationFromToken
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;
    private readonly ApplicationDBContext _context = context;

    public async Task<string> GetManagerId(int departmentId)
    {
        var userDepartment = await _context.UserDepartment
        .FirstOrDefaultAsync(ud => ud.DepartmentId == departmentId);

        if (userDepartment == null)
        {
            throw new Exception("no department with this id");
        }

        var user = await _userManager.FindByIdAsync(userDepartment.UserId);
        if (user == null)
        {
            throw new Exception("no user");
        }

        var isManager = await _userManager.IsInRoleAsync(user, "Manager");
        if (!isManager)
        {
            throw new Exception("there is no manager");
        }
        return user.Id;
    }

    public async Task<UserInfoDto> GetUserIdFromDatabase()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            throw new Exception("HttpContext is null.");
        }

        var user = httpContext.User;

        var userId = user.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? user.FindFirstValue("sub")
            ?? user.FindFirstValue("uid")
            ?? user.Claims.FirstOrDefault(c => c.Type.ToLower().Contains("userid"))?.Value;

        if (string.IsNullOrEmpty(userId))
        {
            var claims = user.Claims.Select(c => new { c.Type, c.Value }).ToList();
            throw new UserIdMissingException("userId is missing in the token.", claims.Select(c => (c.Type, c.Value)).ToList());
        }

        var appUser = await _userManager.FindByIdAsync(userId);
        if (appUser == null)
        {
            throw new Exception($"User not found for ID: {userId}");
        }
        var department = await _context.UserDepartment.FirstAsync(s => s.UserId == userId);

        if (department == null)
        {
            throw new Exception($"department not set for this user ID: {userId}");
        }

        var isCustomer = await _userManager.IsInRoleAsync(appUser, "Manager");


        return new UserInfoDto
        {
            UserId = appUser.Id,
            UserDepartment = department.DepartmentId
        };
    }
}

public class UserIdMissingException : Exception
{
    public List<(string Type, string Value)> Claims { get; }

    public UserIdMissingException(string message, List<(string Type, string Value)> claims) : base(message)
    {
        Claims = claims;
    }
}