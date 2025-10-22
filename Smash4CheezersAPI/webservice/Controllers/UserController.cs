using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using webservice.Controllers.Interfaces;
using webservice.DTO;
using webservice.Exceptions;

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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    public async Task<ActionResult<UserDTO>> GetUser(int id)
    {
        UserDTO user = await _userService.GetUserById(id);

        return Ok(user);
    }


    // POST: api/users/register
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<UserDTO>> Login([FromBody] UserDTO user)
    {
        try
        {
            User? u = await _userService.LoginUser(user);
            return Ok("Vous êtes authentifié !");
        }
        catch (Exception)
        {
            throw new UserException("Les identifiants envoyés sont incorrects");
        }

        return BadRequest();
    }
}