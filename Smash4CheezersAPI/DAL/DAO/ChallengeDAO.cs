using DAL.DAO.Interfaces;
using DAL.Exceptions;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.DAO;

/// <summary>
/// Implements the data access operations for the Challenge entity
/// </summary>
public class ChallengeDAO : IChallengeDAO
{
       private readonly S4CDbContext _context;

       public ChallengeDAO(S4CDbContext context)
       {
              _context = context;
       }

       public async Task<Challenge> Create(Challenge challenge)
       {
              if (challenge == null) throw new ArgumentNullException(nameof(challenge));
              EntityEntry<Challenge> c = _context.Challenges.Add(challenge);
              await _context.SaveChangesAsync();
              _context.Entry(c.Entity).State = EntityState.Detached;
              return c.Entity;
       }

       public async Task<Challenge> Update(Challenge challenge)
       {
              EntityEntry<Challenge> c = _context.Challenges.Update(challenge);
              await _context.SaveChangesAsync();
              _context.Entry(c.Entity).State = EntityState.Detached;
              return c.Entity;
       }

       public async Task<IEnumerable<Challenge>> GetAll()
       {
              return await _context.Challenges.AsNoTracking().ToListAsync() 
                     ?? throw new NotFoundException("No challenges found");
       }

       public async Task<Challenge> GetChallengeById(int id)
       {
             return await _context.Challenges.AsNoTracking().FirstAsync(challenge => challenge.Id == id) 
                    ?? throw new NotFoundException("Challenge not found");
             
       }

       public async Task<Challenge> Delete(int id)
       {
              Challenge challenge = await _context.Challenges.FindAsync(id) ??
                                    throw new NotFoundException("Challenge not found");
              _context.Challenges.Remove(challenge);
              await _context.SaveChangesAsync();
              _context.Entry(challenge).State = EntityState.Detached;
              return challenge;
       }

       public async Task<Challenge> GetChallengeByWeightCategory(string weight)
       {
              return await _context.Challenges.AsNoTracking().FirstOrDefaultAsync(c => c.WeightCategory == weight) ??
                     throw new NotFoundException("Challenge not found");
       }
}