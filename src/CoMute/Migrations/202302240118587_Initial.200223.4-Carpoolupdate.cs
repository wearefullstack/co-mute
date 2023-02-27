namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2002234Carpoolupdate : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarPools", "DepartureTime", c => c.String());
            AlterColumn("dbo.CarPools", "ExpectedArrivalTime", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarPools", "ExpectedArrivalTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CarPools", "DepartureTime", c => c.DateTime(nullable: false));
        }
    }
}
