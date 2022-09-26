using CoMute.Models.Requests;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoMute.Models
{
    public class CarPool : BaseModel
    {
        [Column("DepartureTime")]
        [Required]
        public string DepartureTime { get; set; }

        [Column("ArrivalTime")]
        [Required]
        public string ArrivalTime { get; set; }

        [Column("Origin")]
        [Required]
        public string Origin { get; set; }

        [Column("DaysAvailable")]
        public string DaysAvailable { get; set; }

        [Column("Destination")]
        [Required]
        public string Destination { get; set; }

        [Column("AvailableSeats")]
        [Required]
        public int AvailableSeats { get; set; }

        [Column("OwnerId")]
        [ForeignKey("Users")]
        public int OwnerId { get; set; }

        [Column("Notes")]
        public string Notes { get; set; }
    }
}
