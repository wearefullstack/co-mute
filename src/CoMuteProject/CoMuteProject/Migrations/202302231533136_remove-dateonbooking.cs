namespace CoMuteProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removedateonbooking : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Bookings_Car_Pools", "Date");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Bookings_Car_Pools", "Date", c => c.DateTime(nullable: false));
        }
    }
}
