using DAL.DAO.Interfaces;
using DAL.Exceptions;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.DAO;

/// <summary>
/// Implements the data access operations for the Character entity
/// </summary>
public class CharactersDao : ICharactersDao
{
       private readonly S4CDbContext _context;

       /// <summary>
       ///     Constructor link to table from the db
       /// </summary>
       /// <param name="context">Action for character's table</param>
       public CharactersDao(S4CDbContext context)
       {
              _context = context;
       }

       public async Task<Character> Create(Character character)
       {
              if (character == null)
                     throw new ArgumentNullException(nameof(character));
              EntityEntry<Character> c = _context.Characters.Add(character);
              await _context.SaveChangesAsync();
              _context.Entry(c.Entity).State = EntityState.Detached;
              return c.Entity;
       }

       public async Task<Character> Update(Character character)
       {
              if (character == null)
                     throw new ArgumentNullException(nameof(character));
              EntityEntry<Character> c = _context.Characters.Update(character);
              await _context.SaveChangesAsync();
              _context.Entry(c.Entity).State = EntityState.Detached;
              return c.Entity;
       }

       public async Task<int> Delete(int id)
       {
              Character? character = await _context.Characters.FindAsync(id);
              if (character == null)
                     throw new KeyNotFoundException("Character not found");
              _context.Characters.Remove(character ?? throw new NullReferenceException());
              await _context.SaveChangesAsync();
              _context.Entry(character).State = EntityState.Detached;
              return character.Id;
       }

       public async Task<IEnumerable<Character>> GetAll()
       {
              return await _context.Characters.AsNoTracking().ToListAsync();
       }

       public async Task<Character> GetCharacterById(int id)
       {
              Character c = await _context.Characters.AsNoTracking().Include(c => c.Serie).FirstOrDefaultAsync(c => c.Id == id) ??
                            throw new NotFoundException("Character not found");
              return c;
       }
}