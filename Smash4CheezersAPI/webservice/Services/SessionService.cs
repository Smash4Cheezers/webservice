using DAL.DAO.Interfaces;
using DAL.Models;
using webservice.Controllers.Interfaces.Helpers;
using webservice.Controllers.Interfaces.Services;

namespace webservice.Services;

public class SessionService : ISessionService
{
       private readonly ISessionDAO _sessionDao;
       private readonly ITokenHelper _tokenHelper;
       
       public SessionService(ISessionDAO sessionDao, ITokenHelper tokenHelper)
       {
              _sessionDao = sessionDao;
              _tokenHelper = tokenHelper;
       }

       public async Task<Session?> CreateSession(Session session)
       {
              session.Token = _tokenHelper.GenerateTokenJwt(session.User.Username);
              session.Expiration = DateTime.Now.AddMinutes(20);
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
       
       public Task<Session?> UpdateSession(Session session)
       {
              //TODO : refresh token
              throw new NotImplementedException();
       }
       
       public async Task<Session?> DeleteSession(int id)
       {
              return await _sessionDao.DeleteSession(id);
       }
}