using System.Data;
using DAL.DAO.Interfaces;
using DAL.Exceptions;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.DAO;

/// <inheritdoc />
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
              return s.Entity;
       }

       public async Task<Session?> GetSessionByToken(string token)
       {
              var session = await _context.Sessions.Include(s => s.User).FirstOrDefaultAsync(s => s.Token == token);

              if (session == null) throw new NotFoundException("Session not found");
              return session;
       }

       public async Task<Session?> GetSessionByUserId(int id)
       {
              var session = await _context.Sessions.Include(s => s.User).FirstOrDefaultAsync(s => s.UserId == id);
              if (session == null) throw new NotFoundException("Session not found");
              return session;
       }

       public async Task<IEnumerable<Session>> GetAllSessions()
       {
              return await _context.Sessions.ToListAsync();
       }

       public async Task<Session?> GetSessionById(int id)
       {
              return await _context.Sessions.FindAsync(id) != null
                     ? await _context.Sessions.FindAsync(id)
                     : throw new NotFoundException("Session not found");
       }

       public async Task<Session?> DeleteSession(int id)
       {
              var session =
                     _context.Sessions.Remove(await _context.Sessions.FindAsync(id) ??
                                              throw new NoNullAllowedException());
              await _context.SaveChangesAsync();
              return session.Entity != null
                     ? session.Entity
                     : throw new NotFoundException("Session is already deleted");
       }

       public async Task<Session?> UpdateSession(Session session)
       {
              EntityEntry<Session> s = _context.Sessions.Update(session);
              await _context.SaveChangesAsync();
              return s.Entity;
       }
}