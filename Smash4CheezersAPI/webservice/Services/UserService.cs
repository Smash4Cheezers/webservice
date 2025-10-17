using DAL.DAO;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using webservice.DTO;

namespace webservice.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher<User?> _passwordHasher;
    private readonly IUsersDAO _usersDAO;

    public UserService(IUsersDAO usersDAO, IPasswordHasher<User?> passwordHasher, ICharactersDAO characterDAO)
    {
        _usersDAO = usersDAO;
        _passwordHasher = passwordHasher;
    }

    public async Task<User?> CreateUser(UserDTO user)
    {
        var u = new User
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password
        };
        u.Password = _passwordHasher.HashPassword(u, u.Password);
        return await _usersDAO.Create(u);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        var users = await _usersDAO.GetUsers();
        return users.Select(u => new User
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email
        });
    }

    public async Task<UserDTO> GetUserById(int id)
    {
        var u = await _usersDAO.GetUser(id);
        var character = new CharacterDTO
        {
            Id = u.Character.Id,
            Name = u.Character.Name,
            Weight = u.Character.Weight,
            WeightCategory = u.Character.WeightCategory
        };
        var user = new UserDTO
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            Character = character
        };
        return user;
    }

    public async Task<int> DeleteUser(int id)
    {
        return await _usersDAO.Delete(id);
    }

    public async Task<User?> UpdateUserInformations(int id, UserDTO user)
    {
        var u = new User
        {
            Id = id,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password
        };
        u.Password = _passwordHasher.HashPassword(u, u.Password);
        return await _usersDAO.Update(u);
    }
}