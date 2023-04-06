using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.API.Models
{
    public class Authorization
    {
        public enum Roles
        {
            Admin,//will be responsible for assigning roles to users and adding roles to users
            Lead,// will be responsible for creating car pool opportunities and is considered the lead on the created opportunity
            LeadUser,// will be able to create car pool opportunity and join other car pool opportunities  ut will not be able to join the opportunity that he/she created
            User// will be responsible for joining car pool opportunities
        }

        //default user created with an assigned default role when doing a database migration
        public const string default_name = "Testor";
        public const string default_surname = "comute";
        public const string default_customPhone = "0813386395";
        public const string default_customEmail= "test@comute.co.za";
        public const string default_username = "Testor.comute";
        public const string default_email = "test@comute.co.za";
        public const string default_password = "Pa$$w0rd.";
        public const Roles default_role = Roles.Lead;
    }
}
