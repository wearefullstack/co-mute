namespace Co_Mute.Data
{
    public class Listing
    {
        public Guid Id { get; set; }
        public Guid OpertunityId { get; set; }

        public string UserId { get; set; }

        public DateTime UserJoinDate {get; set; }
    }
}
