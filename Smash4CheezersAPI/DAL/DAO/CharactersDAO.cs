using DAL.Exceptions;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.DAO;

/// <inheritdoc />
public class CharactersDAO : ICharactersDAO
{
    private readonly S4CDbContext _context;

    /// <summary>
    ///     Constructor link to table from the db
    /// </summary>
    /// <param name="context"></param>
    public CharactersDAO(S4CDbContext context)
    {
        _context = context;
    }

    public async Task<Character> Create(Character character)
    {
        if (character == null)
            throw new ArgumentNullException(nameof(character));
        var c = _context.Characters.Add(character);
        await _context.SaveChangesAsync();
        return c.Entity;
    }

    public async Task<Character> Update(Character character)
    {
        if (character == null)
            throw new ArgumentNullException(nameof(character));
        var c = _context.Characters.Update(character);
        await _context.SaveChangesAsync();
        return c.Entity;
    }

    public async Task<int> Delete(int id)
    {
        var character = await _context.Characters.FindAsync(id);
        if (character == null)
            throw new KeyNotFoundException("Character not found");
        _context.Characters.Remove(character ?? throw new NullReferenceException());
        await _context.SaveChangesAsync();
        return character.Id;
    }

    public async Task<IEnumerable<Character>> GetAll()
    {
        return await _context.Characters.ToListAsync();
    }

    public async Task<Character> GetCharacterById(int id)
    {
        var c = await _context.Characters.FindAsync(id);
        if (c == null)
            throw new NotFoundException("Character not found");
        return c;
    }
}