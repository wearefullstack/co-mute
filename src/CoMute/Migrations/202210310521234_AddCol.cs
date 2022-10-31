namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CarPoolMembership", "DateJoined", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CarPoolMembership", "DateJoined");
        }
    }
}
