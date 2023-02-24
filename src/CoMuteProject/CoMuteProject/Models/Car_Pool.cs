using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace CoMuteProject.Models
{
    public class Car_Pool
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Departure Time")]
        [DataType(DataType.Time)]
        public DateTime Departure_Time { get; set; }
        [Required]
        public int Price { get; set; }
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        public DateTime Date_Created { get; set; }
        [Required]
        [Display(Name = "Arrival Time")]
        [DataType(DataType.Time)]
        public DateTime Arrival_Time { get; set; }
        [Required]
        [Display(Name = "Origin")]
        public string Origin { get; set; }
        [Required]
        [Display(Name = "Days Available")]
        public int Days_Available { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        [Display(Name = "Available seats")]
        public int Available_Seats { get; set; }
        [Required]
        public string Owner { get; set; }
        public string Notes { get; set; }
    }
}