using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using webservice.Controllers.Interfaces.Helpers;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace webservice.Helpers;

/// <inheritdoc />
public class TokenHelper : ITokenHelper
{
    private readonly IConfiguration _configuration;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="configuration">dependency injection for configuration</param>
    public TokenHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateTokenJwt(string username)
    {
        IConfigurationSection secretKey = _configuration.GetSection("Jwt:Key") ?? throw new NoNullAllowedException();

        SymmetricSecurityKey key = new SymmetricSecurityKey(Convert.FromBase64String(secretKey.Value ?? throw new NoNullAllowedException()));
        SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        Claim[] claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        JwtSecurityToken token = new JwtSecurityToken
        (
            issuer: "myApp",
            audience: "http://localhost:5183/api/",
            claims: claims,
            expires: DateTime.Now.AddMinutes(20),
            signingCredentials: creds
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}