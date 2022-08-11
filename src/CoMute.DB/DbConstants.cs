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
                    public const string PasswordHash = "PasswordHash";
                    public const string PasswordSalt = "PasswordSalt";
                }
            }

            public class CarPoolOpportunityTable
            {
                public const string TableName = "CarPoolOpportunities";
                public class Columns
                {
                    public const string CarPoolId = "CarPoolId";
                    public const string DepartureTime = "DepartureTime";
                    public const string ExpectedArrivalTime = "ExpectedArrivalTime";
                    public const string Origin = "Origin";
                    public const string DaysAvailable = "DaysAvailable";
                    public const string Destination = "Destination";
                    public const string AvailableSeats = "AvailableSeats";
                    public const string Notes = "Notes";
                    public const string OwnerOrLeader = "OwnerOrLeader";
                    public const string DateCreated = "DateCreated";
                }
            }

            public class JoinCarPoolOpportunityTable
            {
                public const string TableName = "JoinCarPoolOpportunities";
                public class Columns
                {
                    public const string JoinCarPoolsOpportunityId = "JoinCarPoolsOpportunityId";
                    public const string CarPoolId = "CarPoolId";
                    public const string UserId = "UserId";
                    public const string DateJoined = "DateJoined";
                }
            }
        }
    }
}