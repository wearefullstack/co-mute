namespace comute.Models;

public class JoinInfo
{
    public int JoinId { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public int CarPoolId { get; set; }
    public List<CarPool> CarPools { get; set; }
    public DateTime JoinedOn { get; set; }
}
