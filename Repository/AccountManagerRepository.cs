using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Ticket.Dtos.Account;
using Ticket.Interfaces;

namespace Ticket.Repository;
public class AccountManagerRepository(RoleManager<IdentityRole> roleManager) : IAccountManager
{
    private readonly RoleManager<IdentityRole> _roleManager = roleManager;
    public async Task<IdentityResult> AddClaimToRoleAsync(CreateRoleDto dto, Claim claim)
    {
        var role = await _roleManager.FindByNameAsync(dto.RoleName);
        if (role == null)
        {
            throw new Exception("Not found");
        }

        return await _roleManager.AddClaimAsync(role, claim);
    }

    public async Task<IdentityResult> CreateRoleAsync(CreateRoleDto role)
    {
        if (string.IsNullOrWhiteSpace(role.RoleName))
        {
            throw new Exception("Role name cannot be empty.");
        }

        var roleExists = await _roleManager.RoleExistsAsync(role.RoleName);
        if (roleExists)
        {
            throw new Exception("Role already exists.");
        }

        var result = await _roleManager.CreateAsync(new IdentityRole(role.RoleName));
        if (!result.Succeeded)
        {
            throw new Exception("Failed to create the role.");
        }

        return result;
    }

    public async Task<IdentityRole?> FindRoleByNameAsync(GetRoleDto dto)
    {
        return await _roleManager.FindByNameAsync(dto.RoleName);
    }

    public async Task<bool> RoleExistsAsync(string roleName)
    {
        var exists = await _roleManager.RoleExistsAsync(roleName);
        return exists;
    }
}
