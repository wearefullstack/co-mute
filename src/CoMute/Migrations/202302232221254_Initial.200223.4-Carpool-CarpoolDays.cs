namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2002234CarpoolCarpoolDays : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarPoolDays",
                c => new
                    {
                        CarPoolDaysGuid = c.Guid(nullable: false),
                        CarPoolGuid = c.Guid(nullable: false),
                        PoolDay = c.String(),
                    })
                .PrimaryKey(t => t.CarPoolDaysGuid);
            
            AddColumn("dbo.CarPools", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.CarPools", "ModifiedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "CreatedDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "ModifiedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ModifiedDate");
            DropColumn("dbo.Users", "CreatedDate");
            DropColumn("dbo.CarPools", "ModifiedDate");
            DropColumn("dbo.CarPools", "CreatedDate");
            DropTable("dbo.CarPoolDays");
        }
    }
}
