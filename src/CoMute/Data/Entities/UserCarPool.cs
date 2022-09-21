using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CoMute.Web.Data.Entities
{
    public class UserCarPool
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("CarPool")]
        public int CarPoolId { get; set; }
        public string UserId { get; set; }
        public DateTime DateJoined { get; set; }
        public CarPool CarPool { get; set; }
    }
}