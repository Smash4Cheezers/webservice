using DAL.Exceptions;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webservice.Controllers.Interfaces.Services;
using webservice.DTO;
using webservice.Exceptions;

namespace webservice.Controllers;

/// <summary>
/// UserController handles API endpoints related to user operations such as retrieval, creation, update,
/// deletion, and authentication. It acts as a middle layer between the HTTP requests and the business logic
/// in the service layer.
/// </summary>
[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
       private readonly IUserService _userService;
       private readonly ISessionService _sessionService;

       /// <summary>
       /// Constructor
       /// </summary>
       /// <param name="userService">Dependency injection for user service</param>
       /// <param name="sessionService">Dependency injection for session service</param>
       public UserController(IUserService userService, ISessionService sessionService)
       {
              _userService = userService;
              _sessionService = sessionService;
       }

       /// <summary>
       /// Retrieves a collection of all users.
       /// </summary>
       /// <url>GET api/users</url>   
       /// <returns>A task representing the asynchronous operation. The task result contains an action result with the collection of users.</returns>
       [HttpGet]
       public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
       {
              IEnumerable<UserDto?> users = await _userService.GetAllUsers();
              return Ok(users);
       }

       /// <summary>
       /// Retrieves a user by their ID.
       /// </summary>
       /// <url>GET api/users/{id}</url>
       /// <param name="id">The unique identifier of the user to retrieve.</param>
       /// <returns>An ActionResult containing the user data as a UserDTO if found, or an appropriate HTTP response if not found.</returns>
       [HttpGet("{id}")]
       [ProducesResponseType(StatusCodes.Status200OK)]
       [ProducesResponseType(StatusCodes.Status404NotFound)]
       [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
       public async Task<ActionResult<UserDto>> GetUser(int id)
       {
              UserDto user = await _userService.GetUserById(id);
              return Ok(user);
       }


       /// <summary>
       /// Registers a new user in the system.
       /// </summary>
       /// <url>POST api/users/register</url>
       /// <param name="user">The user information to be registered.</param>
       /// <returns>A newly created user in the form of a UserDTO.</returns>
       [HttpPost("register")]
       [AllowAnonymous]
       [ProducesResponseType(StatusCodes.Status201Created)]
       [ProducesResponseType(StatusCodes.Status400BadRequest)]
       public async Task<ActionResult<UserDto>> Register([FromBody] UserDto user)
       {
              ActionResult res;
              if (user == null)
              {
                     res = new BadRequestObjectResult("User object cannot be null");
              }
              else
              {
                     User u = await _userService.CreateUser(user!);
                     UserDto uDto = new UserDto { Id = u.Id, Username = u.Username, Email = u.Email, Password = String.Empty};
                     res = CreatedAtAction(nameof(GetUser), new { id = u.Id }, uDto);
              }
              return res;
       }

       /// <summary>
       /// Deletes the user associated with the specified ID.
       /// </summary>
       /// <url>DELETE api/users/{id}</url>
       /// <param name="id">The unique identifier of the user to be deleted.</param>
       /// <returns>Returns a status code indicating the result of the operation.
       /// If the user is successfully deleted, returns NoContent. If the user is not found, returns NotFound.</returns>
       [HttpDelete("{id}")]
       public async Task<IActionResult> DeleteUser([FromQuery] int id)
       {
              IActionResult res;
              if (id != null)
              {
                     await _userService.DeleteUser(id);
                     res = NoContent();
              }
              else
              {
                     res = NotFound();
              }
              return res;
       }

       [HttpPut("{id}")]
       public async Task<ActionResult<UserDto>> UpdateUser(int id, [FromBody] UserDto user)
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

       /// <summary>
       /// Authenticates a user using the provided login credentials and generates a JWT token upon successful authentication.
       /// </summary>
       /// <url>POST api/users/login</url>
       /// <param name="user">The user object containing login credentials, including Username and Password.</param>
       /// <returns>A UserDTO object containing user details if authentication is successful, along with a generated JWT token.</returns>
       /// <exception cref="UserException">Thrown when the login credentials are invalid or authentication fails.</exception>
       /// <exception cref="TokenException">Thrown when a username is not found or token generation fails.</exception>
       [HttpPost("login")]
       [AllowAnonymous]
       [ProducesResponseType(StatusCodes.Status200OK)]
       [ProducesResponseType(StatusCodes.Status400BadRequest)]
       public async Task<ActionResult<UserDto>> Login([FromBody] AuthDTO user)
       {
              try
              {
                     UserDto? u = await _userService.LoginUser(user);
                     return Ok($"Login successful, hello {u!.Username} !");
              }
              catch (DuplicateEntryException ex)
              {
                     return Conflict(ex.Message);
              }
              catch (Exception e)
              {
                     return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
              }
       }

       [HttpPost("logout")]
       [ProducesResponseType(StatusCodes.Status204NoContent)]
       [ProducesResponseType(StatusCodes.Status401Unauthorized)]
       [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
       public async Task<ActionResult<Session>> Logout([FromHeader] string token)
       {
              ActionResult result;
              Session? session = await _sessionService.GetSessionByToken(token);
              if(session == null)
                     result = Unauthorized(session);
              else
              {
                     await _sessionService.DeleteSession(session.Id);
                     result = NoContent();
              }
              return result;
       }

       [HttpPost("refresh")]
       [ProducesResponseType(StatusCodes.Status204NoContent)]
       [ProducesResponseType(StatusCodes.Status401Unauthorized)]
       [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
       public async Task<ActionResult<Session>> RefreshSession()
       {
              ActionResult result;
              string authHeader = HttpContext.Request.Headers["Authorization"].ToString();
              if(string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer "))
                     return Unauthorized();
              
              string token = authHeader.Substring("Bearer ".Length).Trim();
              Session? session = await _sessionService.GetSessionByToken(token);
              
              if(session == null)
                     result = Unauthorized(session);
              else
              {
                     await _sessionService.UpdateSession(session);
                     result = NoContent();
              }
              return result;
       }
}