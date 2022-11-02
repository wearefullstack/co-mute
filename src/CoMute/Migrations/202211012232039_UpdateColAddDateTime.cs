namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateColAddDateTime : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CarPool", "DepartureTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.CarPool", "ExpectedArrivalTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CarPool", "ExpectedArrivalTime", c => c.String(nullable: false));
            AlterColumn("dbo.CarPool", "DepartureTime", c => c.String(nullable: false));
        }
    }
}
