namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Final3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Carpool", "PassengerIds", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Carpool", "PassengerIds");
        }
    }
}
