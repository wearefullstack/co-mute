using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoMute.Web.Models.Dto
{
    public class CarPoolDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Depature Time is required")]
        [Display(Name ="Departure Time")]
        public DateTime DepartureTime { get; set; }

        [Required(ErrorMessage = "Expected Arrival Time is required")]
        [Display(Name ="Expected Arrival Time")]
        public DateTime ExpectedArrivalTime { get; set; }

        [Required(ErrorMessage = "Origin is required")]
        public string Origin { get; set; }

        [Required(ErrorMessage = "Destination is required")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Available Seats required")]
        [Display(Name = "Available Seats")]
        public int AvailableSeats { get; set; }

        public string Notes { get; set; }

        [Required(ErrorMessage = "Owner / Leader required")]
        [Display(Name = "Owner / Leader")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Available Days required")]
        [Display(Name = "Available Days")]
        public ICollection<AvailableDay> AvailableDays { get; set; }
    }
}