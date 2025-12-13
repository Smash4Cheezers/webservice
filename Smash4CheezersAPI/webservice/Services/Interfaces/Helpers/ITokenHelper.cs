namespace webservice.Services.Interfaces.Helpers;

/// <summary>
/// Interface defining the token helper
/// </summary>
public interface ITokenHelper
{
       /// <summary>
       /// Generates a JSON Web Token (JWT) for a specified username.
       /// </summary>
       /// <param name="username">The username for which the JWT will be generated.</param>
       /// <returns>A string representation of the generated JWT.</returns>
       public string GenerateTokenJwt(string username);

       /// <summary>
       /// Generate a refresh token
       /// </summary>
       /// <param name="expiration">the expiration of the current token given</param>
       /// <param name="username">username provided</param>
       /// <returns>new token</returns>
       public string GenerateRefreshToken(DateTime expiration, string username);
}