namespace comute.Models;

public class JoinInfo
{
    public int JoinId { get; set; }
    public int UserId { get; set; }
    public User User = new();
    public int CarPoolId { get; set; }
    public List<CarPool> CarPools = new();
    public int JoinedOn { get; set; }
}
