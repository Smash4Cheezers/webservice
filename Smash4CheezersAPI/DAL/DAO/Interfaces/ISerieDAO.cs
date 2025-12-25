using DAL.Models;

namespace DAL.DAO.Interfaces;

/// <summary>
/// Interface defining data access operations for Serie entities
/// </summary>
public interface ISerieDAO
{
       /// <summary>
       /// Get all series
       /// </summary>
       /// <returns>A collection of series</returns>
       Task<IEnumerable<Serie>> GetAll();
       
       /// <summary>
       /// Get a serie by its id
       /// </summary>
       /// <param name="id">ID of the serie</param>
       /// <returns>The serie specified</returns>
       Task<Serie> GetSerieById(int id);
}