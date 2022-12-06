
namespace Co_Mute.Data
{
    public class Oppertunities
    {
        public Guid Id { get; set; }
        public DateTime DepartTime { get; set; }
        public DateTime ExpectedArrival { get; set; }
        public string Origin { get; set; }
        public Guid OwnerId { get; set; }

        public string Notes { get; set; }

    }
}
