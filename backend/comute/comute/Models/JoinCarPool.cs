using System.ComponentModel.DataAnnotations;

namespace comute.Models;

public class JoinCarPool
{
    [Key]
    public int JoinId { get; set; }
    [Required]
    public int UserId { get; set; }
    [Required]
    public int CarPoolId { get; set; }
    public DateTime JoinedOn { get; set; }

    public JoinCarPool(int joinId,int userId,int carPoolId,DateTime joinedOn)
    {
        JoinId = joinId;
        UserId = userId;
        CarPoolId = carPoolId;
        JoinedOn = joinedOn;
    }

    public JoinCarPool() { }
   
}
