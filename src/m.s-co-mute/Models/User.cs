﻿namespace m.s_co_mute.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string? Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User()
        {

        }
    }
}