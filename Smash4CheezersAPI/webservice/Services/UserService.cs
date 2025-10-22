using DAL.DAO.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.Elfie.Extensions;
using webservice.Controllers.Interfaces;
using webservice.DTO;
using webservice.Exceptions;

namespace webservice.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUsersDAO _usersDao;
    private readonly ICharacterService _characterService;

    /// <summary>
    /// Dependencies injection
    /// </summary>
    /// <param name="usersDao">DAO injected</param>
    /// <param name="passwordHasher">Tool for hash a password and verify a password</param>
    public UserService(IUsersDAO usersDao, IPasswordHasher<User> passwordHasher, ICharacterService characterService)
    {
        _usersDao = usersDao;
        _passwordHasher = passwordHasher;
        _characterService = characterService;
    }

    public async Task<User?> CreateUser(UserDTO user)
    {
        var u = new User
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password = _passwordHasher.HashPassword(null, user.Password)
        };
        return await _usersDao.Create(u);
    }

    public async Task<User?> LoginUser(UserDTO user)
    {
        User u = await _usersDao.GetUser(user.Id);
        return _passwordHasher.VerifyHashedPassword(u, u.Password, user.Password) == PasswordVerificationResult.Success
            ? u
            : throw new UserException("Current password is incorrect");
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        IEnumerable<User?> users = await _usersDao.GetUsers();
        return users.Select(u => new User
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email
        }) ?? throw new UserException("No users found");
    }

    public async Task<UserDTO> GetUserById(int id)
    {
        User user = await _usersDao.GetUser(id);
        if(user == null) throw new UserException("User not found");
        UserDTO userDto = new UserDTO
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Character = await _characterService.GetCharacterById(user.CharacterID == null ? 0 : user.CharacterID.Value)
        };
        return userDto;
    }

    public async Task<int> DeleteUser(int id)
    {
        return await _usersDao.Delete(id);
    }

    public async Task<User?> UpdateUserInformations(int id, UserDTO user)
    {
        User u = new User
        {
            Id = id,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password = _passwordHasher.HashPassword(null, user.Password)
        };
        return await _usersDao.Update(u);
    }
}