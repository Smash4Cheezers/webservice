using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using webservice.Exceptions;
using webservice.Services.Interfaces.Helpers;
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
              IConfigurationSection secretKey =
                     _configuration.GetSection("Jwt:Key") ?? throw new NoNullAllowedException();

              SymmetricSecurityKey key =
                     new SymmetricSecurityKey(
                            Convert.FromBase64String(secretKey.Value ?? throw new NoNullAllowedException()));
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
                     expires: DateTime.Now.AddDays(2),
                     signingCredentials: creds
              );
              return new JwtSecurityTokenHandler().WriteToken(token);
       }

       public string GenerateRefreshToken(DateTime expiration, string username)
       {
              if(string.IsNullOrEmpty(username))
                     throw new ArgumentNullException(nameof(username));
              if(expiration == default)
                     throw new ArgumentNullException(nameof(expiration));
              
              DateTime now = DateTime.UtcNow;
              int result = DateTime.Compare(expiration, now);
              return result < 0 ? throw new TokenException("Token expired") : GenerateTokenJwt(username);
       }
}