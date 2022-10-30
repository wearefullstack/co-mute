namespace CoMute.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserTableAlterCols : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.User", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.User", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.User", "EmailAddress", c => c.String(nullable: false));
            AlterColumn("dbo.User", "Password", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.User", "Password", c => c.String());
            AlterColumn("dbo.User", "EmailAddress", c => c.String());
            AlterColumn("dbo.User", "Surname", c => c.String());
            AlterColumn("dbo.User", "Name", c => c.String());
        }
    }
}
