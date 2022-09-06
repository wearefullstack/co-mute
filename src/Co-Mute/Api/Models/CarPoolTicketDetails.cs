namespace Co_Mute.Api.Models
{
    public class CarPoolTicketDetails : CarPoolTicket
    {
        public List<UserDetail> Passengers { get; set; }
        public int Cid { get; set; }
    }
}
