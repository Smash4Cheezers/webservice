using DAL.DAO.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using webservice.Controllers.Interfaces.Services;
using webservice.DTO;
using webservice.Exceptions;

namespace webservice.Services;

public class UserService : IUserService
{
       private readonly IPasswordHasher<User> _passwordHasher;
       private readonly IUsersDao _usersDao;
       private readonly ISessionService _sessionService;
       private readonly ICharacterService _characterService;

       /// <summary>
       /// Dependencies injection
       /// </summary>
       /// <param name="usersDao">DAO injected</param>
       /// <param name="passwordHasher">Tool for hash a password and verify a password</param>
       /// <param name="characterService">Needed to access data from the Character table</param>
       /// <param name="sessionService"></param>
       public UserService(IUsersDao usersDao, IPasswordHasher<User> passwordHasher, ICharacterService characterService,
              ISessionService sessionService)
       {
              _usersDao = usersDao;
              _passwordHasher = passwordHasher;
              _characterService = characterService;
              _sessionService = sessionService;
       }

       public async Task<User> CreateUser(UserDto user)
       {
              User u = new User
              {
                     Id = user.Id,
                     Username = user.Username,
                     Email = user.Email,
                     Password = _passwordHasher.HashPassword(null, user.Password)
              };
              return await _usersDao.Create(u) ?? throw new UserException("Impossible to create the user");
       }

       public async Task<UserDto?> LoginUser(AuthDTO user)
       {
              User u = await _usersDao.GetUserByUsername(user.Username) ??
                       throw new UserException("Username not found");
              if (_passwordHasher.VerifyHashedPassword(u, u.Password, user.Password) !=
                  PasswordVerificationResult.Success)
              {
                     throw new UserException("Current password is incorrect");
              }

              UserDto userDto = new()
              {
                     Id = u.Id,
                     Username = u.Username,
                     Email = u.Email,
                     Character = u.CharacterId == null
                            ? null
                            : await _characterService.GetCharacterById(u.CharacterId.Value)
              };
              return userDto;
       }

       public async Task<IEnumerable<UserDto?>> GetAllUsers()
       {
              IEnumerable<User?> u = await _usersDao.GetUsers();
              return u.Select(user => new UserDto
              {
                     Id = user!.Id,
                     Username = user.Username,
                     Email = user.Email,
              }) ?? throw new UserException("No users found");
       }

       public async Task<UserDto> GetUserById(int id)
       {
              User user = await _usersDao.GetUser(id);
              if (user == null) throw new UserException("User not found");
              UserDto userDto = new UserDto
              {
                     Id = user.Id,
                     Username = user.Username,
                     Email = user.Email,
                     Character = user.CharacterId == null
                            ? null
                            : await _characterService.GetCharacterById(user.CharacterId.Value)
              };
              return userDto;
       }

       public async Task<User> DeleteUser(int id)
       {
              return await _usersDao.Delete(id);
       }

       public async Task<User?> UpdateUserInformations(int id, UserDto user)
       {
              User u = new User
              {
                     Id = id,
                     Username = user.Username,
                     Email = user.Email,
                     Password = _passwordHasher.HashPassword(null, user.Password)
              };
              return await _usersDao.Update(u);
       }
}