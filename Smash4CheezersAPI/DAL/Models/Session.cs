using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public class Session
{
       [Key]
       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       public int Id { get; init; }
       [Required] public string Token { get; set; } = null!;
       [Required] public DateTime Expiration { get; set; }
       [ForeignKey(nameof(User))] [Required] public int UserId { get; set; }
       public User User { get; set; } = null!;
}