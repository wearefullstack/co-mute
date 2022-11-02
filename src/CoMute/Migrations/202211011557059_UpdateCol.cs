namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarPool", "CreatedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarPool", "CreatedDate");
        }
    }
}
