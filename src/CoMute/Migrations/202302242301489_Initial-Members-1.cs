namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMembers1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarPoolMembers",
                c => new
                    {
                        CarPoolMemberGuid = c.Guid(nullable: false),
                        CarPoolGuid = c.Guid(nullable: false),
                        UserGuid = c.Guid(nullable: false),
                        JoinedDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        StatusDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.CarPoolMemberGuid);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CarPoolMembers");
        }
    }
}
