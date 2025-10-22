using DAL.DAO.Interfaces;
using DAL.Models;
using webservice.Controllers.Interfaces;
using webservice.DTO;

namespace webservice.Services;

/// <summary>
/// Handle the data access operations for the Character entity
/// </summary>
public class CharacterService : ICharacterService
{
    private readonly ICharactersDAO _charactersDao;
    
    /// <summary>
    /// Constructor (dependencies injection)
    /// </summary>
    /// <param name="charactersDao">Dependency injection</param>
    public CharacterService(ICharactersDAO charactersDao)
    {
        _charactersDao = charactersDao;
    }
    
    public async Task<IEnumerable<CharacterDTO?>> GetAllCharacters()
    {
        IEnumerable<Character?> characters = await _charactersDao.GetAll();
        return characters.Select(chara => new CharacterDTO
        {
            Id = chara.Id,
            Name = chara.Name,
            Weight = chara.Weight,
            WeightCategory = chara.WeightCategory,
        });
    }

    public async Task<CharacterDTO?> GetCharacterById(int id)
    {
        Character character = await _charactersDao.GetCharacterById(id) ?? throw new NullReferenceException();
        CharacterDTO characterDto = new CharacterDTO
        {
            Id = character.Id,
            Name = character.Name,
            Weight = character.Weight,
            WeightCategory = character.WeightCategory
        };
        return characterDto;
    }
}