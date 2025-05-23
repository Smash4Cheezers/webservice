using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DAO;

public class CharactersDAO
{
    private readonly S4CDbContext _context;

    public CharactersDAO(S4CDbContext context)
    {
        _context = context;
    }

    public async Task Create(Character character)
    {
        if(character == null)
            throw new ArgumentNullException(nameof(character));
        _context.Characters.Add(character);
        await _context.SaveChangesAsync();
    }

    public async Task Update(Character character)
    {
        if(character == null)
            throw new ArgumentNullException(nameof(character));
        _context.Characters.Update(character);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(int id)
    {
        var character = await _context.Characters.FindAsync(id);
        if(character == null)
            throw new KeyNotFoundException("Character not found");
        _context.Characters.Remove(character ?? throw new NullReferenceException());
        await _context.SaveChangesAsync();
    }

    public async Task<List<Character>> GetAll()
    {
        return await _context.Characters.ToListAsync();
    }
}