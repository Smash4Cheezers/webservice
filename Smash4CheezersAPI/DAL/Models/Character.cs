using System.ComponentModel.DataAnnotations;

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
    ///     Its serie
    /// </summary>
    public string Serie { get; set; }
}