using co_mute_be.Abstractions.Enums;

namespace co_mute_be.Models
{

    public class CarPoolBooking
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime BookedOn { get; set; }
        public string CarPoolRef { get; set; }
    }


}
