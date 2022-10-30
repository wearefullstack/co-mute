using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoMute.Web.Models
{
    public class CarPoolMembership
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int CarPoolId { get; set; }
    }
}