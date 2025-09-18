using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string Username { get; set; }
    
    [Required]
    public string Password { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [ForeignKey(nameof(Character))]
    public int? CharacterID { get; set; }
    
    public Character? Character { get; set; }
}