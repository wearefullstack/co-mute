using System;
using ansyl.dao;

namespace CoMute.Lib.Dao.comute
{
    //  this is the data object for the User table
    class User : IDaoObject
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegisterTime { get; set; }
        public long GetPrimaryId() => UserId;
    }
}
