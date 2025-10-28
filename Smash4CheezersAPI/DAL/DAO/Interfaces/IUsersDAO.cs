using System.Data;
using DAL.Exceptions;
using DAL.Models;

namespace DAL.DAO.Interfaces;

/// <summary>
///     Interface defining data access operations for Users entities
/// </summary>
public interface IUsersDao
{
    /// <summary>
    ///     Create a user in a new row in the users table
    /// </summary>
    /// <param name="user">The user to create</param>
    Task<User?> Create(User user);

    /// <summary>
    ///     Update a user in the users table
    /// </summary>
    /// <param name="user">The user to update</param>
    Task<User?> Update(User user);

    /// <summary>
    ///     Delete a user
    /// </summary>
    /// <param name="id">id of the user</param>
    /// <exception cref="NoNullAllowedException"></exception>
    Task<int> Delete(int id);

    /// <summary>
    ///     Retrieve a user by an id
    /// </summary>
    /// <param name="id">the id of the user</param>
    /// <returns>The user</returns>
    /// <exception cref="NotFoundException">Throw it when a user isn't found</exception>
    Task<User> GetUser(int id);

    /// <summary>
    ///     Get all users
    /// </summary>
    /// <returns>A list of users</returns>
    Task<IEnumerable<User?>> GetUsers();

    /// <summary>
    ///     Get a user by his character
    /// </summary>
    /// <param name="id">Current ID of the character</param>
    /// <returns>Users who have the current character id in main character player</returns>
    Task<User?> GetUsersByCharacter(int id);
}