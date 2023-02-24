namespace CoMuteProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adddatecreatedandprice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Car_Pool", "Price", c => c.Int(nullable: false));
            AddColumn("dbo.Car_Pool", "Date_Created", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Car_Pool", "Date_Created");
            DropColumn("dbo.Car_Pool", "Price");
        }
    }
}
