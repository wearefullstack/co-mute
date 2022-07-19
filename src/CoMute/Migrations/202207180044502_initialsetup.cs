namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialsetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarPools",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        DepartureTime = c.DateTime(nullable: false),
                        ArrivalTime = c.DateTime(nullable: false),
                        Origin = c.String(nullable: false),
                        Destination = c.String(nullable: false),
                        DaysAvailable = c.Int(nullable: false),
                        SeatsAvailable = c.Int(nullable: false),
                        Owner = c.String(nullable: false),
                        Notes = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CarPools");
        }
    }
}
