using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;
using webservice.DTO;
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

       public string GenerateAccessToken(UserDto userDto)
       {
              IConfigurationSection secretKey =
                     _configuration.GetSection("Jwt:Key") ?? throw new NoNullAllowedException();

              SymmetricSecurityKey key =
                     new SymmetricSecurityKey(
                            Convert.FromBase64String(secretKey.Value ?? throw new NoNullAllowedException()));
              SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
             
                     List<Claim> claims = new()
                     {
                            new Claim(JwtRegisteredClaimNames.Sub, userDto.Id.ToString()),
                            new Claim(ClaimTypes.Name, userDto.Username),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                     };
                     
                     if(userDto.Character != null)
                            claims.Add(new Claim("CharacterId", userDto.Character.Id.ToString()));

              JwtSecurityToken token = new JwtSecurityToken
              (
                     issuer: "myApp",
                     audience: "https://localhost:7020/api/",
                     claims: claims,
                     expires: DateTime.Now.AddMinutes(15),
                     signingCredentials: creds
              );
              return new JwtSecurityTokenHandler().WriteToken(token);
       }

       public string GenerateRefreshToken()
       {
              return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
       }
}