
namespace CoMute.Web.Models.Dto
{
    public class RideJoinRequest //Join
    {
        public int RideId { get; set; }
        public int UserId { get; set; }
    }

    public class RideLeaveRequest //Leave
    {
        public int RideId { get; set; }
        public int UserId { get; set; }
    }

    public class RideDeleteRequest //Delete
    {
        public int RideId { get; set; }
        public int UserId { get; set; }
    }

}