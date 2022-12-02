namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Final1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carpool", "UserID", c => c.String(nullable: false));
            AlterColumn("dbo.Carpool", "UserName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Carpool", "UserName", c => c.String(nullable: false));
            DropColumn("dbo.Carpool", "UserID");
        }
    }
}
