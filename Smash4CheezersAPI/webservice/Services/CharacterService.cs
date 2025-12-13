using DAL.DAO.Interfaces;
using DAL.Models;
using webservice.Controllers.Interfaces.Services;
using webservice.DTO;

namespace webservice.Services;

/// <summary>
/// Handle the data access operations for the Character entity
/// </summary>
public class CharacterService : ICharacterService
{
    private readonly ICharactersDao _charactersDao;
    private readonly ISerieService _serieService;
    
    /// <summary>
    /// Constructor (dependencies injection)
    /// </summary>
    /// <param name="charactersDao">Dependency injection</param>
    public CharacterService(ICharactersDao charactersDao, ISerieService serieService)
    {
        _charactersDao = charactersDao;
        _serieService = serieService;
    }

    public async Task<IEnumerable<CharacterDto?>> GetAllCharacters()
    {
        IEnumerable<Character?> characters = await _charactersDao.GetAll();
        return characters.Select(chara => new CharacterDto()
        {
            Id = chara.Id,
            Name = chara.Name,
            Weight = chara.Weight,
            WeightCategory = chara.WeightCategory,
            Serie = chara.Serie != null
                ? new SerieDTO()
                {
                    Id = chara.SerieId,
                    Name = chara.Serie.Name
                }
                : null,
        });
    }

    public async Task<CharacterDto?> GetCharacterById(int id)
    {
        Character character = await _charactersDao.GetCharacterById(id) ?? throw new NullReferenceException();
        CharacterDto characterDto = new CharacterDto
        {
            Id = character.Id,
            Name = character.Name,
            Weight = character.Weight,
            WeightCategory = character.WeightCategory,
            Serie = await _serieService.GetSerieById(character.SerieId),
        };
        return characterDto;
    }
}