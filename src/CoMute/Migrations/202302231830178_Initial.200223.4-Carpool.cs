namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2002234Carpool : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarPools",
                c => new
                    {
                        CarPoolGuid = c.Guid(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        ExpectedArrivalTime = c.DateTime(nullable: false),
                        Origin = c.String(),
                        Destination = c.String(),
                        AvailableSeats = c.Int(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.CarPoolGuid);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserGuid = c.Guid(nullable: false),
                        FirstName = c.String(),
                        Surname = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.UserGuid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
            DropTable("dbo.CarPools");
        }
    }
}
