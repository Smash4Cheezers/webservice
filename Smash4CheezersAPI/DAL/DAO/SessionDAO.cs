using System.Data;
using DAL.DAO.Interfaces;
using DAL.Exceptions;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.DAO;

/// <summary>
/// Implements the data access operations for the Session entity
/// </summary>
public class SessionDao : ISessionDao
{
       private readonly S4CDbContext _context;

       public SessionDao(S4CDbContext context)
       {
              _context = context;
       }

       public async Task<Session> CreateSession(Session session)
       {
              EntityEntry<Session> s = await _context.Sessions.AddAsync(session);
              await _context.SaveChangesAsync();
              _context.Entry(s.Entity).State = EntityState.Detached;
              return s.Entity;
       }

       public async Task<Session?> GetSessionByToken(string token)
       {
              Session? session = await _context.Sessions.AsNoTracking().Include(s => s.User).FirstOrDefaultAsync(s => s.Token == token);

              if (session == null) throw new NotFoundException("Session not found");
              return session;
       }

       public async Task<Session?> GetSessionByUserId(int id)
       {
              Session? session = await _context.Sessions.AsNoTracking().Include(s => s.User).FirstOrDefaultAsync(s => s.UserId == id);
              if (session == null) throw new NotFoundException("Session not found");
              return session;
       }

       public async Task<IEnumerable<Session>> GetAllSessions()
       {
              return await _context.Sessions.AsNoTracking().ToListAsync();
       }

       public async Task<Session?> GetSessionById(int id)
       {
              return await _context.Sessions.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id)
                     ?? throw new NotFoundException("Session not found");
       }

       public async Task<Session?> DeleteSession(int id)
       {
              EntityEntry<Session> session =
                     _context.Sessions.Remove(await _context.Sessions.FindAsync(id) ??
                                              throw new NoNullAllowedException());
              await _context.SaveChangesAsync();
              _context.Entry(session.Entity).State = EntityState.Detached;
              return session.Entity != null
                     ? session.Entity
                     : throw new NotFoundException("Session is already deleted");
       }

       public async Task<Session?> UpdateSession(Session session)
       {
              EntityEntry<Session> s = _context.Sessions.Update(session);
              await _context.SaveChangesAsync();
              _context.Entry(s.Entity).State = EntityState.Detached;
              return s.Entity;
       }
}