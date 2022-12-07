namespace Co_Mute.Models
{
    public class CreateOppertunityPostModal
    {
        public string Notes { get; set; }

        public string Origin { get; set; }
        public string Destination { get; set; }
        public int NumSeats { get; set; }
        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Sunday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; }

        public string DepartTime { get; set; }
        public string ArrivalTime { get; set; }
    }
}
