using webservice.DTO;

namespace webservice.Services.Interfaces.Helpers;

/// <summary>
/// Interface defining the token helper
/// </summary>
public interface ITokenHelper
{
       /// <summary>
       /// Generates a JSON Web Token (JWT) for a specified username.
       /// </summary>
       /// <param name="userDto">The user for which to generate the JWT.</param>
       /// <returns>A string representation of the generated JWT.</returns>
       public string GenerateAccessToken(UserDto userDto);

       /// <summary>
       /// Generate a refresh token
       /// </summary>
       /// <returns>new token</returns>
       public string GenerateRefreshToken();
}