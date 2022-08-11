using System;
using System.ComponentModel.DataAnnotations;

namespace CoMute.Web.Models.Dto
{
    public class CarPoolsOpportunityRequest
    {
        public Guid CarPoolId { get; set; }
        [Required]
        [Display(Name = "Departure Time")]
        public DateTime DepartureTime { get; set; }
        [Required]
        [Display(Name = "Expected Arrival Time")]
        public DateTime ExpectedArrivalTime { get; set; }
        [Display(Name = "Date Created")]
        public DateTime DateCreated { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0")]
        [Display(Name = "Days Available")]
        public int DaysAvailable { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0")]
        [Display(Name = "Available Seats")]
        public int AvailableSeats { get; set; }
        public string Notes { get; set; }
        [Display(Name = "Owner/Leader")]
        public string OwnerOrLeader { get; set; }
        public bool CanJoin { get; set; }
        public string UserId { get; set; }
        public bool HasAvailableSeats { get; set; }
    }
}