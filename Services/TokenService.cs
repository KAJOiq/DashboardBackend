using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Ticket.Dtos.Account;
using Ticket.Interfaces;
using Ticket.Models;

namespace Ticket.Services;
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly SymmetricSecurityKey _key;
    private readonly IOptions<JwtSettings> _jwtSettings;
    public TokenService(IConfiguration configuration,IOptions<JwtSettings> jwtSettings)
    {
        _jwtSettings = jwtSettings.Value;
        _configuration = configuration;
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"]));
    }

    public string CreateToken(User user)
    {
        if (!string.IsNullOrEmpty(user.Email) || !string.IsNullOrEmpty(user.UserName))
        {
            throw new InvalidOperationException("unable to find the email or user name");
        }

        var claims = new List<Claim>{
            new(JwtRegisteredClaimNames.Email , user.Email!),
            new(JwtRegisteredClaimNames.GivenName, user.UserName!)
        };

        var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.Now.AddDays(7),
            SigningCredentials = credentials,
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"]
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}
