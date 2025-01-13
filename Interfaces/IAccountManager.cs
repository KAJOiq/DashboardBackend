using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Ticket.Dtos.Account;

namespace Ticket.Interfaces;
public interface IAccountManager
{
    Task<bool> RoleExistsAsync(string roleName);
    Task<IdentityResult> CreateRoleAsync(CreateRoleDto role);
    Task<IdentityRole?> FindRoleByNameAsync(GetRoleDto dto);
    Task<IdentityResult> AddClaimToRoleAsync(CreateRoleDto dto, Claim claim);
}
