using System.Data;
using DAL.DAO.Interfaces;
using DAL.Exceptions;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DAO;

/// <inheritdoc cref="IUsersDAO" />
public class UsersDAO : IUsersDAO
{
    private readonly S4CDbContext _context;


    public UsersDAO(S4CDbContext context)
    {
        _context = context;
    }


    public async Task<User?> Create(User user)
    {
        var u = await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return u.Entity;
    }


    public async Task<User?> Update(User user)
    {
        var u = _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return u.Entity;
    }

    public async Task<int> Delete(int id)
    {
        var u = _context.Users.Remove(await _context.Users.FindAsync(id) ?? throw new NoNullAllowedException());
        await _context.SaveChangesAsync(); 
        return u.Entity.Id != null ? u.Entity.Id : throw new NotFoundException("User not found");
    }

    public async Task<User> GetUser(int id)
    {
        var user = await _context.Users.Include(u => u.Character).FirstOrDefaultAsync(u => u.Id == id);

        if (user == null) throw new NotFoundException("User not found");
        return user;
    }

    public async Task<IEnumerable<User?>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUsersByCharacter(int id)
    {
        var u = _context.Users.Include(u => u.Character).FirstOrDefaultAsync(u => u.Id == id);
        if (u == null) throw new NotFoundException("Currently no users found");
        return await u;
    }
}