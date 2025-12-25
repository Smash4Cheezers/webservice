using DAL.DAO.Interfaces;
using DAL.Models;
using webservice.Controllers.Interfaces.Services;
using webservice.DTO;
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

       public async Task<Session?> CreateSession(UserDto user, string token)
       {
              if (user == null) throw new ArgumentNullException(nameof(user));
              Session session = new Session
              {
                     UserId = user.Id,
                     Token = token,
                     Expiration = DateTime.UtcNow.AddDays(7)
              };
              return await _sessionDao.CreateSession(session);
       }

       public async Task<Session> GetSessionByToken(string token)
       {
              return await _sessionDao.GetSessionByToken(token);
       }

       public async Task<Session?> GetSessionByUserId(int id)
       {
              return await _sessionDao.GetSessionByUserId(id);
       }

       public async Task<Session?> DeleteSession(int id)
       {
              return await _sessionDao.DeleteSession(id);
       }

       public string GenerateAccessToken(UserDto user)
       {
              return _tokenHelper.GenerateAccessToken(user);
       }

       public string GenerateRefreshToken()
       {
              return _tokenHelper.GenerateRefreshToken();
       }

       public async Task DeleteByToken(string refreshToken)
       {
              if (string.IsNullOrWhiteSpace(refreshToken)) throw new TokenException("Not an existing token");
              await _sessionDao.DeleteSessionByToken(refreshToken);
       }

       public async Task<Session> RefreshSession(string refreshToken)
       {
              if (string.IsNullOrEmpty(refreshToken))
                     throw new TokenException("No refresh token provided");
              Session session = await _sessionDao.GetSessionByToken(refreshToken);

              if (session.Expiration < DateTime.UtcNow)
                     throw new TokenException("Refresh token expired");
              session.Token = _tokenHelper.GenerateRefreshToken();
              session.Expiration = DateTime.UtcNow.AddDays(7);

              return await _sessionDao.UpdateSession(session)
                     ?? throw new TokenException("Invalid session");
       }
}