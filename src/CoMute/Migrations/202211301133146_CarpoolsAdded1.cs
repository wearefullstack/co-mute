namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarpoolsAdded1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Course", newName: "Carpool");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Carpool", newName: "Course");
        }
    }
}
