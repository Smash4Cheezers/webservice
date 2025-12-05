using DAL.Models;

namespace DAL.DAO.Interfaces;

/// <summary>
/// Defines the data access operations for the Challenge entity.
/// </summary>
/// <remarks>
/// Provides methods to create, retrieve, delete, and query Challenge entities in the persistence layer.
/// </remarks>
public interface IChallengeDAO
{
       /// <summary>
       /// Creates a new challenge record in the data source.
       /// </summary>
       /// <param name="challenge">The challenge object to be added to the data source. It must contain the necessary details such as name, description, and weight category.</param>
       /// <returns>A task that represents the asynchronous operation. The task result contains the newly created challenge object, including its generated ID.</returns>
       Task<Challenge> Create(Challenge challenge);

       /// <summary>
       /// Updates an existing challenge record in the data source.
       /// </summary>
       /// <param name="challenge">Challenge to modify</param>
       /// <returns>the challenge updated</returns>
       Task<Challenge> Update(Challenge challenge);

       /// <summary>
       /// Retrieves a collection of all challenges.
       /// </summary>
       /// <returns>
       /// A task representing the asynchronous operation. The task result contains
       /// an enumerable collection of <see cref="Challenge"/> objects.
       /// </returns>
       Task<IEnumerable<Challenge>> GetAll();

       /// <summary>
       /// Retrieves a challenge by its unique identifier.
       /// </summary>
       /// <param name="id">The unique identifier of the challenge to retrieve.</param>
       /// <returns>The challenge that matches the specified identifier if found; otherwise, null.</returns>
       Task<Challenge> GetChallengeById(int id);

       /// <summary>
       /// Deletes a challenge with the specified identifier.
       /// </summary>
       /// <param name="id">The unique identifier of the challenge to be deleted.</param>
       /// <returns>
       /// A task that represents the asynchronous operation. The task result contains
       /// the challenge instance that was deleted. If no challenge was found with the
       /// specified identifier, the result may be null.
       /// </returns>
       Task<Challenge> Delete(int id);

       /// <summary>
       /// Retrieves a challenge that corresponds to the specified weight category.
       /// </summary>
       /// <param name="weight">The weight category used to identify the challenge.</param>
       /// <returns>The challenge that matches the specified weight category.</returns>
       Task<Challenge> GetChallengeByWeightCategory(string weight);
}