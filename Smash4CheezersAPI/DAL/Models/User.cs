using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; init; }

    [Required] public string Username { get; set; }

    [Required] public string Password { get; set; }

    [Required] public string Email { get; set; }

    [ForeignKey(nameof(Character))] public int? CharacterID { get; set; }

    public Character? Character { get; set; }
}