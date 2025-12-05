using DAL.DAO.Interfaces;
using DAL.Exceptions;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.DAO;

/// <summary>
/// Implements the data access operations for the Serie entity
/// </summary>
public class SerieDAO : ISerieDAO
{
       private readonly S4CDbContext _context;

       /// <summary>
       /// Provides data access operations for the Serie entity, including CRUD operations.
       /// </summary>
       /// <remarks>
       /// This class interacts with the database context to perform operations on the Series table.
       /// </remarks>
       public SerieDAO(S4CDbContext context)
       {
              _context = context;
       }

       public async Task<IEnumerable<Serie>> GetAll()
       {
              return await _context.Series.AsNoTracking().ToListAsync() ?? 
                     throw new NotFoundException("No series found");
       }

       public async Task<Serie> GetSerieById(int id)
       {
              return await _context.Series.AsNoTracking().FirstOrDefaultAsync(s => s.Id == id) ?? 
                     throw new NotFoundException("Serie not found");
       }
}