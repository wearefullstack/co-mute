using System;
using FluentMigrator;

namespace CoMute.DB.Migrations.Migrations
{
    [Migration(202208032113)]
    public class _202208032113_InitialMigration : Migration
    {
        public override void Up()
        {
            Create.Table(DbConstants.Tables.UserTable.TableName)
                .WithColumn(DbConstants.Tables.UserTable.Columns.UserId).AsGuid().PrimaryKey()
                .WithColumn(DbConstants.Tables.UserTable.Columns.Name).AsString(512).NotNullable()
                .WithColumn(DbConstants.Tables.UserTable.Columns.Surname).AsString(int.MaxValue).NotNullable()
                .WithColumn(DbConstants.Tables.UserTable.Columns.EmailAddress).AsString(int.MaxValue).NotNullable()
                .WithColumn(DbConstants.Tables.UserTable.Columns.PhoneNumber).AsString(10).Nullable()
                .WithColumn(DbConstants.Tables.UserTable.Columns.PasswordHash).AsBinary(int.MaxValue).NotNullable()
                .WithColumn(DbConstants.Tables.UserTable.Columns.PasswordSalt).AsBinary(int.MaxValue).NotNullable();
        }

        public override void Down()
        {
        }
    }
}