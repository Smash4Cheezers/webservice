using DAL.DAO.Interfaces;
using DAL.Models;
using webservice.Controllers.Interfaces.Services;
using webservice.DTO;

namespace webservice.Services;

public class SerieService : ISerieService
{
       private readonly ISerieDAO  _serieDAO;

       public SerieService(ISerieDAO serieDao)
       {
              _serieDAO = serieDao;
       }

       /// <summary>
       /// Get a serie by id and make an instance of SerieDTO
       /// </summary>
       /// <param name="id">ID of the serie needed</param>
       /// <returns>SerieDTO</returns>
       public async Task<SerieDTO> GetSerieById(int id)
       {
              Serie serie = await _serieDAO.GetSerieById(id);
              return new SerieDTO()
              {
                     Id = serie.Id,
                     Name = serie.Name,
              };
       }
}