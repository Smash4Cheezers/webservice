using DAL.DAO.Interfaces;
using DAL.Exceptions;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.DAO;

/// <summary>
/// Implements the data access operations for the User entity
/// </summary>
public class UsersDao : IUsersDao
{
    private readonly S4CDbContext _context;


    public UsersDao(S4CDbContext context)
    {
        _context = context;
    }
    
    public async Task<User?> Create(User user)
    {
        EntityEntry<User> u = await _context.Users.AddAsync(user);
        
        try
        {
            await _context.SaveChangesAsync();
            return u.Entity;
        }
        catch (DbUpdateException e)
        {
            if(e.InnerException is MySqlConnector.MySqlException)
                throw new DuplicateEntryException("Username or Email already exists");
            throw new DbUpdateException("Unable to save user", e);
        }
    }

    public async Task<User?> Update(User user)
    {
        _context.Entry(user).State = EntityState.Modified;
        try
        {
            await _context.SaveChangesAsync();
            return user;
        }
        catch (DbUpdateException e)
        {
            if(e.InnerException is MySqlConnector.MySqlException)
                throw new DuplicateEntryException("Username or Email already exists");
            throw new DbUpdateException("Unable to save user", e);
        }
    }

    public async Task<User> Delete(int id)
    {
        User user = new() { Id = id };
        _context.Users.Attach(user);
        EntityEntry<User> u = _context.Users.Remove(user);
        if(await _context.SaveChangesAsync() == 0)
            throw new NotFoundException("User not found or already deleted");
        return u.Entity;
    }

    public async Task<User> GetUser(int id)
    {
        User? user = await _context.Users.AsNoTracking().Include(u => u.Character).FirstOrDefaultAsync(u => u.Id == id);
        if (user == null) throw new NotFoundException("User not found");
        return user;
    }

    public async Task<IEnumerable<User?>> GetUsers()
    {
        return await _context.Users.AsNoTracking()
            .Select(u => new User()
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            CharacterId = u.CharacterId,
            Password = u.Password,
        }).ToListAsync();
    }

    public async Task<User?> GetUsersByCharacter(int id)
    {
        User? u = await _context.Users.Include(u => u.Character).FirstOrDefaultAsync(u => u.CharacterId == id);
        if (u == null) throw new NotFoundException("Currently no users found");
        return u;
    }

    public async Task<User?> GetUserByUsername(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Username == username) ??  throw new NotFoundException("Username not found");
    }
}