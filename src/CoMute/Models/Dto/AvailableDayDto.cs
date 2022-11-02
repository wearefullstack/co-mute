using System.Collections.Generic;

namespace CoMute.Web.Models.Dto
{
    public class AvailableDayDto
    {
        public int Id { get; set; }
        public DayEnumeration Day { get; set; }
        public int CarPoolId { get; set; }
    }
}