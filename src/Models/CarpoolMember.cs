namespace FSWebApi.Models
{
    public class CarpoolMember
    {
        public Guid CarpoolId { get; set; }
        public Guid UserId { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
