namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CarPoolTbs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AvailableDay",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.Int(nullable: false),
                        CarPoolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarPool", t => t.CarPoolId, cascadeDelete: true)
                .Index(t => t.CarPoolId);
            
            CreateTable(
                "dbo.CarPool",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DepartureTime = c.DateTime(nullable: false),
                        ExpectedArrivalTime = c.DateTime(nullable: false),
                        Origin = c.String(nullable: false),
                        Destination = c.String(nullable: false),
                        AvailableSeats = c.Int(nullable: false),
                        Notes = c.String(),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CarPoolMembership",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        CarPoolId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AvailableDay", "CarPoolId", "dbo.CarPool");
            DropForeignKey("dbo.CarPool", "UserId", "dbo.User");
            DropForeignKey("dbo.CarPoolMembership", "UserId", "dbo.User");
            DropIndex("dbo.CarPoolMembership", new[] { "UserId" });
            DropIndex("dbo.CarPool", new[] { "UserId" });
            DropIndex("dbo.AvailableDay", new[] { "CarPoolId" });
            DropTable("dbo.CarPoolMembership");
            DropTable("dbo.CarPool");
            DropTable("dbo.AvailableDay");
        }
    }
}
