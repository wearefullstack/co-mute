using FluentMigrator;
namespace CoMute.DB.Migrations.Migrations
{
    [Migration(202208041458)]
    public class _202208041458_AddJoinCarPoolsTable : Migration
    {
            public override void Up()
            {
            Create.Table(DbConstants.Tables.JoinCarPoolOpportunityTable.TableName)
                .WithColumn(DbConstants.Tables.JoinCarPoolOpportunityTable.Columns.JoinCarPoolsOpportunityId).AsGuid().PrimaryKey()
                .WithColumn(DbConstants.Tables.JoinCarPoolOpportunityTable.Columns.CarPoolId).AsGuid().NotNullable()
                .WithColumn(DbConstants.Tables.JoinCarPoolOpportunityTable.Columns.UserId).AsGuid().NotNullable()
                .WithColumn(DbConstants.Tables.JoinCarPoolOpportunityTable.Columns.DateJoined).AsDateTime().NotNullable();
            }

            public override void Down()
            {
            }
        }
    }