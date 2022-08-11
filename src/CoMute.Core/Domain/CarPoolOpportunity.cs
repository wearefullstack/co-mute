using System;
using System.ComponentModel.DataAnnotations;

namespace CoMute.Core.Domain
{
    public class CarPoolOpportunity
    {
        public Guid CarPoolId { get; set; }
        [Required]
        public DateTime DepartureTime { get; set; }
        [Required]
        public DateTime ExpectedArrivalTime { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public int DaysAvailable { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public int AvailableSeats { get; set; }
        public string Notes { get; set; }
        [Required]
        public string OwnerOrLeader { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
