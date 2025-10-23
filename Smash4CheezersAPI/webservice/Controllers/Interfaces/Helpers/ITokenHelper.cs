namespace webservice.Controllers.Interfaces.Helpers;

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
}