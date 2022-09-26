using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace CoMute.Models
{
  public class UserCarPool : BaseModel
  {
    [Column("UserId")]
    [ForeignKey("Users")]
    [Required]
    public int UserId { get; set; }

    [Column("CarPoolId")]
    [ForeignKey("CarPools")]
    [Required]
    public int CarPoolId { get; set; }

    public UserCarPool(int userId, int carPoolId)
    {
      UserId = userId;
      CarPoolId = carPoolId;
    }
  }
}
