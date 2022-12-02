namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarpoolsAdded2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Carpool", "Owner_UserID", "dbo.Users");
            DropIndex("dbo.Carpool", new[] { "Owner_UserID" });
            AddColumn("dbo.Carpool", "UserName", c => c.String(nullable: false));
            DropColumn("dbo.Carpool", "Owner_UserID");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
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
            
            AddColumn("dbo.Carpool", "Owner_UserID", c => c.Int(nullable: false));
            DropColumn("dbo.Carpool", "UserName");
            CreateIndex("dbo.Carpool", "Owner_UserID");
            AddForeignKey("dbo.Carpool", "Owner_UserID", "dbo.Users", "UserID", cascadeDelete: true);
        }
    }
}
