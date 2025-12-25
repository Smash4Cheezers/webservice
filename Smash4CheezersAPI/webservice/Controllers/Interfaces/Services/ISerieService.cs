using webservice.DTO;

namespace webservice.Controllers.Interfaces.Services;

public interface ISerieService
{
       /// <summary>
       /// Get a serie by id and make an instance of SerieDTO
       /// </summary>
       /// <param name="id">ID of the serie needed</param>
       /// <returns>SerieDTO</returns>
       Task<SerieDTO> GetSerieById(int id);
}