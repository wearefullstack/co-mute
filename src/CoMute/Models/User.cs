using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CoMute.Models
{
  public class User : BaseModel
  {
    [Column("Name")]
    [Required]
    public string Name { get; set; }

    [Column("Surname")]
    [Required]
    public string Surname { get; set; }

    [Column("Email")]
    [Required]
    public string Email { get; set; }

    [Column("Phone")]
    public string Phone { get; set; }

    [Column("Password")]
    [Required]
    public string Password { get; set; }

    [Column("Token")]
    public string Token { get; set; }

  }
}
