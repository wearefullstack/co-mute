
using System;

namespace CoMute.Web.Models.Dto
{
    public sealed class CarpoolCreationRequest
    {
        public string Origin { get; set; }
        public string Destination { get; set;}
        public string DaysAvailable { get; set; }
        public int AvailableSeats { get; set; }
        public TimeSpan DepartureTime { get; set; }
        public TimeSpan ExpectedTime { get; set; }
        public string TheNotes { get; set; }
    }
}