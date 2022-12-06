
namespace Co_Mute.Data
{
    public class Oppertunities
    {
        public Guid Id { get; set; }
        public DateTime DepartTime { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public DateTime CreateDate { get; set; }
        public string Origin { get; set; }
        public string OwnerId { get; set; }

        public bool Monday { get; set; }
        public bool Tuesday { get; set; }
        public bool Wednesday { get; set; }
        public bool Sunday { get; set; }
        public bool Thursday { get; set; }
        public bool Friday { get; set; }
        public bool Saturday { get; set; } 
        public string Notes { get; set; }

        public int NumberOfSeats { get; set; }

    }
}
