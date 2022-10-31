using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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

        [Display(Name = "Date Joined")]
        public DateTime DateJoined { get; set; }

        public int CarPoolMembershipId { get; set; }

        public string AvailableDaysToString()
        {
            if(AvailableDays.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach(AvailableDay availableDay in AvailableDays)
                {
                    switch(availableDay.Day)
                    {
                        case DayEnumeration.Monday:
                            sb.Append(nameof(DayEnumeration.Monday) + "\n");
                            break;
                        case DayEnumeration.Tuesday:
                            sb.Append(nameof(DayEnumeration.Tuesday) + "\n");
                            break;
                        case DayEnumeration.Wedsnesday:
                            sb.Append(nameof(DayEnumeration.Wedsnesday) + "\n");
                            break;
                        case DayEnumeration.Thursday:
                            sb.Append(nameof(DayEnumeration.Thursday) + "\n");
                            break;
                        case DayEnumeration.Friday:
                            sb.Append(nameof(DayEnumeration.Friday) + "\n");
                            break;
                        case DayEnumeration.Saturday:
                            sb.Append(nameof(DayEnumeration.Saturday) + "\n");
                            break;
                        case DayEnumeration.Sunday:
                            sb.Append(nameof(DayEnumeration.Sunday) + "\n");
                            break;
                    }
                }
                return sb.ToString();
            }
            return "No days available";
        }
    }
}