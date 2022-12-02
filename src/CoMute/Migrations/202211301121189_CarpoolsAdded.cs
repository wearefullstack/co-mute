namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarpoolsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CarpoolID = c.Int(nullable: false, identity: true),
                        DepartureTime = c.DateTime(nullable: false),
                        ETA = c.DateTime(nullable: false),
                        Origin = c.String(nullable: false),
                        Destination = c.String(nullable: false),
                        iAvailableSeats = c.Int(nullable: false),
                        Notes = c.String(),
                        Owner_UserID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CarpoolID)
                .ForeignKey("dbo.Users", t => t.Owner_UserID, cascadeDelete: true)
                .Index(t => t.Owner_UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        EmailAddress = c.String(),
                        PhoneNumber = c.String(),
                        Password = c.String(),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Course", "Owner_UserID", "dbo.Users");
            DropIndex("dbo.Course", new[] { "Owner_UserID" });
            DropTable("dbo.Users");
            DropTable("dbo.Course");
        }
    }
}
