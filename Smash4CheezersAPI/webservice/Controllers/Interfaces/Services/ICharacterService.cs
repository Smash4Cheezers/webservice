using webservice.DTO;

namespace webservice.Controllers.Interfaces.Services;

/// <summary>
/// Interface defining data access operations for Character entities
/// </summary>
public interface ICharacterService
{
    /// <summary>
    ///     Get all characters
    /// </summary>
    /// <returns>Collection of characters</returns>
    Task<IEnumerable<CharacterDTO?>> GetAllCharacters();

    /// <summary>
    ///     Get a character by its id
    /// </summary>
    /// <param name="id">id of the character</param>
    /// <returns>the character</returns>
    Task<CharacterDTO?> GetCharacterById(int id);
}