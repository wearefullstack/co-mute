using Microsoft.Build.Framework;

namespace Co_Mute.Data
{
    public class Oppertunities
    {
        public Guid Id { get; set; }
        [Required]
        public DateTime DepartTime { get; set; }
        [Required]
        public DateTime ExpectedArrival { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public Guid OwnerId { get; set; }

        public string Notes { get; set; }

    }
}
