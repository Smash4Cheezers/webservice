using DAL.DAO.Interfaces;
using DAL.Models;
using webservice.Controllers.Interfaces.Services;
using webservice.Exceptions;
using webservice.Services.Interfaces.Helpers;

namespace webservice.Services;

public class SessionService : ISessionService
{
       private readonly ISessionDao _sessionDao;
       private readonly ITokenHelper _tokenHelper;
              
       public SessionService(ISessionDao sessionDao, ITokenHelper tokenHelper)
       {
              _sessionDao = sessionDao;
              _tokenHelper = tokenHelper;
       }

       public async Task<Session?> CreateSession(User user)
       {
              Session session = new Session() { User = user!, UserId = user!.Id };
              session.Token = _tokenHelper.GenerateTokenJwt(session.User.Username);
              session.Expiration = DateTime.UtcNow .AddDays(2);
              return await _sessionDao.CreateSession(session);
       }
       
       public async Task<Session?> GetSessionByToken(string token)
       {
              return await _sessionDao.GetSessionByToken(token);
       }
       
       public async Task<Session?> GetSessionByUserId(int id)
       {
              return await _sessionDao.GetSessionByUserId(id);
       }
       
       public Task<Session> UpdateSession(Session session)
       {
              string res = _tokenHelper.GenerateRefreshToken(session.Expiration, session.User.Username);
              session.Token = res;
              return _sessionDao.UpdateSession(session) ?? throw new TokenException("Can't update an invalid session");
       }
       
       public async Task<Session?> DeleteSession(int id)
       {
              return await _sessionDao.DeleteSession(id);
       }
}