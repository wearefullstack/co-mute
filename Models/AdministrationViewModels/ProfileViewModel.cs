namespace Co_Mute.Models.AdministrationViewModels
{
    public class ProfileViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string Role { get; set; }

        public string Surname { get; set; }

        public  string Email { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public DateTime Year { get; set; }

        public string School { get; set; }
        public Guid QualificationId { get; set; }
        public string PhoneNumber { get; set; }

    }
}
