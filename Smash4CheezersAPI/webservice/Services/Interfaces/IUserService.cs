using DAL.Models;
using webservice.DTO;

namespace webservice.Services;

/// <summary>
/// Manage the result from controller with some tools and reformat information to the DAO
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Hash the password and give the user received to the DAO to create him in the database
    /// </summary>
    /// <param name="user">user provided</param>
    /// <returns>User completed</returns>
    Task<User> CreateUser(UserDTO user);

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>Collection of users</returns>
    Task<IEnumerable<User>> GetAllUsers();

    /// <summary>
    /// Get a user by its id
    /// </summary>
    /// <param name="id">id of the user</param>
    /// <returns>the user</returns>
    Task<User> GetUserById(int id);
}