using DAL.Models;

namespace DAL.DAO;

/// <summary>
///     Interface defining data access operations for Character entities
/// </summary>
public interface ICharactersDAO
{
    /// <summary>
    ///     Creates a new character in the database
    /// </summary>
    /// <param name="character">The character entity to create</param>
    /// <returns>The created character with generated ID and database values</returns>
    /// <exception cref="ArgumentNullException">Thrown when character parameter is null</exception>
    Task<Character> Create(Character character);

    /// <summary>
    ///     Updates an existing character in the database
    /// </summary>
    /// <param name="character">The character entity with updated information</param>
    /// <returns>The updated character entity</returns>
    /// <exception cref="ArgumentNullException">Thrown when character parameter is null</exception>
    Task<Character> Update(Character character);

    /// <summary>
    ///     Deletes a character from the database by its identifiers
    /// </summary>
    /// <param name="id">The unique identifier of the character to delete</param>
    /// <returns>The ID of the deleted character</returns>
    /// <exception cref="KeyNotFoundException">Thrown when no character is found with the specified ID</exception>
    Task<int> Delete(int id);

    /// <summary>
    ///     Retrieves all characters from the database
    /// </summary>
    /// <returns>A list containing all character entities</returns>
    Task<IEnumerable<Character>> GetAll();

    Task<Character> GetCharacterById(int id);
}