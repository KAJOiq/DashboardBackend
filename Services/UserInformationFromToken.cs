using System.Security.Claims;
using DashboardBackend.Interfaces;
using Microsoft.AspNetCore.Identity;
using projects.Dtos.Account;
using projects.Models;

namespace projects.Services;
public class UserInformationFromToken(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor) : IUserInformationFromToken
{
    private readonly UserManager<User> _userManager = userManager;
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    
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

        return new UserInfoDto
        {
            UserId = appUser.Id,
        };
    }

    Task<Ticket.Dtos.Account.UserInfoDto> IUserInformationFromToken.GetUserIdFromDatabase()
    {
        throw new NotImplementedException();
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