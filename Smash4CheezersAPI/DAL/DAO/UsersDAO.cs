using System.Data;
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
        await _context.SaveChangesAsync();
        _context.Entry(user).State = EntityState.Detached;
        return u.Entity;
    }


    public async Task<User?> Update(User user)
    {
        EntityEntry<User> u = _context.Users.Update(user);
        await _context.SaveChangesAsync();
        _context.Entry(u).State = EntityState.Detached;
        return u.Entity;
    }

    public async Task<User> Delete(int id)
    {
        EntityEntry<User> u = _context.Users.Remove(await _context.Users.FindAsync(id) ?? throw new NoNullAllowedException());
        await _context.SaveChangesAsync(); 
        _context.Entry(u.Entity).State = EntityState.Detached;
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
        return await _context.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetUsersByCharacter(int id)
    {
        Task<User?>? u = _context.Users.AsNoTracking().Include(u => u.Character).FirstOrDefaultAsync(u => u.Id == id);
        if (u == null) throw new NotFoundException("Currently no users found");
        return await u;
    }
}