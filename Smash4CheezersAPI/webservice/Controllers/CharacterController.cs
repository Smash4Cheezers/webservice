using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using webservice.Controllers.Interfaces.Services;
using webservice.DTO;

namespace webservice.Controllers;

[Authorize(AuthenticationSchemes = "Bearer")]
[Route("api/characters")]
[ApiController]
public class CharacterController : ControllerBase
{
    private readonly ICharacterService _characterService;

    public CharacterController(ICharacterService characterService)
    {
        _characterService = characterService;
    }

    // GET: api/Character
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
    public async Task<ActionResult<IEnumerable<Character>>> GetCharacters()
    {
        IEnumerable<CharacterDto?> characters = await _characterService.GetAllCharacters();
        return Ok(characters);
    }

    // GET: api/Character/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CharacterDto>> GetCharacter(int id)
    {
        CharacterDto? character = await _characterService.GetCharacterById(id);

        if (character == null) return NotFound();

        return character;
    }
    
}