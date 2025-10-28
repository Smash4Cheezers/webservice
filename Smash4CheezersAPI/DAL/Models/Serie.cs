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
}