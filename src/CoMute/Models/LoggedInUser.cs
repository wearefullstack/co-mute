using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Models
{
    public static class LoggedInUser
    {
        public static   int Id { get; set; }
        public static string Name { get; set; }
        public static string Surname { get; set; }
        public static string Email { get; set; }
        public static string Password { get; set; }
        public static string PhoneNumber { get; set; }
    }
}