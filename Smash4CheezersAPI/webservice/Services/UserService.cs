using DAL.DAO;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using webservice.DTO;

namespace webservice.Services;

public class UserService : IUserService
{
    private IUsersDAO _usersDAO;
    private IPasswordHasher<User> _passwordHasher;

    public UserService(IUsersDAO usersDAO, IPasswordHasher<User> passwordHasher)
    {
        this._usersDAO = usersDAO;
        this._passwordHasher = passwordHasher;
    }

    public async Task<User> CreateUser(UserDTO user)
    {
        User u = new User
        {
            Username = user.Username,
            Email = user.Email,
            Password = user.Password,
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

    public async Task<User> GetUserById(int id)
    {
        return await _usersDAO.GetUser(id);
    }
}