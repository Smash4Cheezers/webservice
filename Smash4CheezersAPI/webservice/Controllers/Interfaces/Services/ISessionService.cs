using DAL.Models;
using webservice.DTO;

namespace webservice.Controllers.Interfaces.Services;

/// <summary>
/// Interface for managing user session operations,
/// such as creation, retrieval, update, and deletion of sessions.
/// </summary>
public interface ISessionService
{
       /// <summary>
       /// Creates a new session for a user with an associated token and expiration time.
       /// </summary>
       /// <param name="user"></param>
       /// <param name="token"></param>
       /// <returns>A task representing the asynchronous operation. The task result contains the created <see cref="Session"/> object if successful, otherwise null.</returns>
       Task<Session?> CreateSession(UserDto user, string token);

       /// <summary>
       /// Retrieves a session based on the specified token.
       /// </summary>
       /// <param name="token">The token associated with the session to be retrieved.</param>
       /// <returns>A <see cref="Session"/> object if found; otherwise, null.</returns>
       Task<Session> GetSessionByToken(string token);

       /// <summary>
       /// Retrieves the session associated with the specified user ID.
       /// </summary>
       /// <param name="id">The unique identifier of the user whose session is being retrieved.</param>
       /// <returns>
       /// A <see cref="Session"/> object representing the session of the user if found;
       /// otherwise, null if no session is associated with the specified user ID.
       /// </returns>
       Task<Session?> GetSessionByUserId(int id);

       /// <summary>
       /// Deletes a session by its unique identifier.
       /// </summary>
       /// <param name="id">The unique identifier of the session to be deleted.</param>
       /// <returns>The deleted session if it exists; otherwise, null.</returns>
       Task<Session?> DeleteSession(int id);
       
       /// <summary>
       /// Generate an access token for a user
       /// </summary>
       /// <param name="user">User's access token</param>
       /// <returns>the access token</returns>
       string GenerateAccessToken(UserDto user);
       
       /// <summary>
       /// Generate a refresh token
       /// </summary>
       /// <returns>the refresh token</returns>
       string GenerateRefreshToken();

       /// <summary>
       /// Delete a session by its refresh token
       /// </summary>
       /// <param name="refreshToken">refresh token</param>
       /// <returns></returns>
       Task DeleteByToken(string refreshToken);

       /// <summary>
       /// Refresh a session
       /// </summary>
       /// <param name="refreshToken">refresh token provided</param>
       /// <returns>new session</returns>
       public Task<Session> RefreshSession(string refreshToken);

}