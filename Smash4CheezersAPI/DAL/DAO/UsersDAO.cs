using System.Data;
using DAL.Exceptions;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DAO;

/// <summary>
/// Users table data accessor 
/// </summary>
public class UsersDAO
{
    private readonly S4CDbContext _context;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">Database link</param>
    public UsersDAO(S4CDbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Create a user in a new row in the users table
    /// </summary>
    /// <param name="user">The user to create</param>
    public async Task Create(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Update a user in the users table
    /// </summary>
    /// <param name="user">The user to update</param>
    public async Task Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Delete a user
    /// </summary>
    /// <param name="id">id of the user</param>
    public async Task Delete(int id)
    {
        _context.Users.Remove(await _context.Users.FindAsync(id) ?? throw new NoNullAllowedException());
        await _context.SaveChangesAsync();
    }
    
    /// <summary>
    /// Retrieve a user by an id
    /// </summary>
    /// <param name="id">the id of the user</param>
    /// <returns>The user</returns>
    /// <exception cref="NotFoundException">Throw it when a user isn't found</exception>
    public async Task<User> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            throw new NotFoundException("User not found");
        }
        return user;
    }

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns>A list of users</returns>
    public async Task<IEnumerable<User>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }
}