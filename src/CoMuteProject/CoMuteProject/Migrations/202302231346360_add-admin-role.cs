namespace CoMuteProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addadminrole : DbMigration
    {
        public override void Up()
        {
            Sql(@"
                  INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Admin')
                  INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [Name], [Surname]) VALUES (N'699d16cd-988c-4822-96db-fabd16e90c47', N'admin@admin', 0, N'APrjXKuQjQ4O7YQY6SADRtoeaR7aDGRqT7G0XNfZGR5t4orUZmtyoM262J5g5IJsUA==', N'4f7ffb46-a21e-4a80-a8e1-9fd5f796bfb5', NULL, 0, 0, NULL, 1, 0, N'admin@admin', NULL, NULL)
                  INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'699d16cd-988c-4822-96db-fabd16e90c47', N'1')
             ");
        }
        
        public override void Down()
        {
        }
    }
}
