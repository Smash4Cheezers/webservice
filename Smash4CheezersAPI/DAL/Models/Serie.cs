using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models;

/// <summary>
/// Represents a serie entity.
/// </summary>
public class Serie
{
       /// <summary>
       /// Id of the serie
       /// </summary>
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int Id { get; set; }
       
       /// <summary>
       /// Name of the serie
       /// </summary>
       [Required] public string Name { get; set; }

       /// <summary>
       /// Collection of challenges associated with the serie.
       /// </summary>
       /// <remarks>
       /// This property represents the relationship between a series and the challenges
       /// that belong to it. Each challenge in the collection is linked to the current serie.
       /// </remarks>
       public ICollection<Challenge> Challenges { get; set; }
}