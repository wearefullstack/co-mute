using System.ComponentModel.DataAnnotations;

namespace CoMute.Web.Models
{
    public class AvailableDay
    {
        [Key]
        public int Id { get; set; }
        public DayEnumeration Day {get; set;}
        public int CarPoolId { get; set; }
        public virtual CarPool CarPool { get; set; }
    }
}