using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CoMute.Models
{
  public class BaseModel
  {
    [Column("Id")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int Id { get; set; }

    [Column("Created")]
    public DateTime Created { get; set; }
    
    [Column("Modified")]
    [JsonIgnore]
    public DateTime Modified { get; set; }
  }
}
