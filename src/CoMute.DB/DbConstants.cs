namespace CoMute.DB
{
    public class DbConstants
    {
        public class Tables
        {
            public class UserTable
            {
                public const string TableName = "User";
                public class Columns
                {
                    public const string UserId = "UserId";
                    public const string Name = "Name";
                    public const string Surname = "Surname";
                    public const string EmailAddress = "EmailAddress";
                    public const string PhoneNumber = "PhoneNumber";
                    public const string Password = "Password";
                }
            }            
        }
    }
}