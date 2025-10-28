using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models;

public class Character
{
    /// <summary>
    ///     Its ID
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    ///     Name of the character
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    ///     Light, Medium, Heavy or super Heavy Weight
    /// </summary>
    [Required]
    public string WeightCategory { get; set; }

    /// <summary>
    ///     Weight, as the number
    /// </summary>
    [Required]
    public int Weight { get; set; }

    /// <summary>
    ///   its serie id
    /// </summary>
    [ForeignKey(nameof(Serie))]
    [Required]
    public int SerieId { get; set; }
    
    /// <summary>
    /// Represents the serie to which the character belongs.
    /// </summary>
    public Serie Serie {get; set;} = null!;
}