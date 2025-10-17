using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using webservice.DTO;
using webservice.Services;

namespace webservice.Controllers;

[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        var users = await _userService.GetAllUsers();
        return Ok(users);
    }

    // GET: api/users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDTO>> GetUser(int id)
    {
        var user = await _userService.GetUserById(id);

        if (user == null) return NotFound();

        return Ok(user);
    }


    // POST: api/users/register
    [HttpPost("register")]
    public async Task<ActionResult<UserDTO>> Register([FromBody] UserDTO user)
    {
        try
        {
            var u = await _userService.CreateUser(user);
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    //DELETE: api/users/?
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var u = await _userService.DeleteUser(id);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<UserDTO>> UpdateUser(int id, [FromBody] UserDTO user)
    {
        if (id != user.Id) return NotFound(id);

        try
        {
            await _userService.UpdateUserInformations(id, user);
            return NoContent();
        }
        catch (Exception e)
        {
            return NotFound();
        }
    }

    // api/users/login
    [HttpPost("login")]
    public async Task<ActionResult<UserDTO>> Login([FromBody] UserDTO user)
    {
        if (user == null) return BadRequest();

        try
        {
            var u = await _userService.GetUserById(user.Id);
            if (user.Password == u.Password && user.Username == u.Username && user.Email == u.Email)
                return Ok("Vous êtes authentifié !");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return BadRequest();
    }
}