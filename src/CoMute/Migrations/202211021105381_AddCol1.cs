namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCol1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarPool", "MaximumSeats", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarPool", "MaximumSeats");
        }
    }
}
