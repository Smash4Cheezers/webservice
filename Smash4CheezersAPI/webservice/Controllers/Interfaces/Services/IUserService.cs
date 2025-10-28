using DAL.Models;
using webservice.DTO;
using webservice.Exceptions;

namespace webservice.Controllers.Interfaces.Services;

/// <summary>
///     Manage the result from the controller with some tools and reformat information to the DAO
/// </summary>
public interface IUserService
{
    /// <summary>
    ///     Hash the password and give the user received to the DAO to create him in the database
    /// </summary>
    /// <param name="user">user provided</param>
    /// <returns>User completed</returns>
    Task<User> CreateUser(UserDto user);

    /// <summary>
    ///     Get all users
    /// </summary>
    /// <returns>Collection of users</returns>
    Task<IEnumerable<UserDto?>> GetAllUsers();

    /// <summary>
    ///     Get a user by his id
    /// </summary>
    /// <param name="id">id of the user</param>
    /// <returns>the user</returns>
    Task<UserDto> GetUserById(int id);

    /// <summary>
    ///     Delete a user by his id
    /// </summary>
    /// <param name="id">id of the user</param>
    /// <returns>User deleted</returns>
    Task<int> DeleteUser(int id);

    /// <summary>
    /// Update user informations
    /// </summary>
    /// <param name="id">user id</param>
    /// <param name="user">user information</param>
    /// <returns></returns>
    Task<User?> UpdateUserInformations(int id, UserDto user);

    /// <summary>
    /// Verify if the user is in the database and if the password is correct
    /// </summary>
    /// <param name="user">User informations from client-side</param>
    /// <exception cref="UserException">Returns a not found</exception>
    /// <returns>A token if informations are true and existing, a 404 if not</returns>
    public Task<User?> LoginUser(UserDto user);
}