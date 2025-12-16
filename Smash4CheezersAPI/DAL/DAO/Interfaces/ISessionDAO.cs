using DAL.Models;

namespace DAL.DAO.Interfaces;

/// <summary>
/// Defines methods for managing and accessing session data in the data layer.
/// </summary>
public interface ISessionDao
{
       /// <summary>
       /// Creates a new session associated with the provided token.
       /// </summary>
       /// <param name="session">Session to create</param>
       /// <returns>The created <see cref="Session"/> object containing session details.</returns>
       Task<Session> CreateSession(Session session);

       /// <summary>
       /// Get a session by its token
       /// </summary>
       /// <param name="token">token provided</param>
       /// <returns>a session</returns>
       Task<Session> GetSessionByToken(string token);

       /// <summary>
       /// Retrieves a session associated with a specific user by the user's identifier.
       /// </summary>
       /// <param name="id">The unique identifier of the user whose session is to be retrieved.</param>
       /// <returns>
       /// A <see cref="Session"/> object representing the session associated with the specified user if found;
       /// otherwise, null if no session exists for the given user identifier.
       /// </returns>
       Task<Session?> GetSessionByUserId(int id);

       /// <summary>
       /// Retrieves all sessions stored in the data layer.
       /// </summary>
       /// <returns>A collection of all <see cref="Session"/> objects.</returns>
       Task<IEnumerable<Session>> GetAllSessions();

       /// <summary>
       /// Retrieves a session from the data layer based on the given session ID.
       /// </summary>
       /// <param name="id">The unique identifier of the session to retrieve.</param>
       /// <returns>The session associated with the specified ID, or null if no session is found.</returns>
       Task<Session?> GetSessionById(int id);

       /// <summary>
       /// Deletes a session by its unique identifier.
       /// </summary>
       /// <param name="id">The unique identifier of the session to delete.</param>
       /// <returns>The deleted session if it exists; otherwise, null.</returns>
       Task<Session?> DeleteSession(int id);

       /// <summary>
       /// Updates an existing session with new details provided in the session object.
       /// </summary>
       /// <param name="session">The session object containing updated information.</param>
       /// <returns>The updated <see cref="Session"/> object with the applied changes, or null if the session does not exist.</returns>
       Task<Session> UpdateSession(Session session);


       /// <summary>
       /// Delete a session by its refresh token
       /// </summary>
       /// <param name="refreshToken">refresh token</param>
       Task DeleteSessionByToken(string refreshToken);
}