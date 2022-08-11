using FluentMigrator;

namespace CoMute.DB.Migrations.Migrations
{
    [Migration(202208032152)]
    public class _202208032152_AddCarPoolsTable : Migration
    {
        public override void Up()
        {
            Create.Table(DbConstants.Tables.CarPoolOpportunityTable.TableName)
                .WithColumn(DbConstants.Tables.CarPoolOpportunityTable.Columns.CarPoolId).AsGuid().PrimaryKey()
                .WithColumn(DbConstants.Tables.CarPoolOpportunityTable.Columns.DepartureTime).AsDateTime().NotNullable()
                .WithColumn(DbConstants.Tables.CarPoolOpportunityTable.Columns.ExpectedArrivalTime).AsDateTime().NotNullable()
                .WithColumn(DbConstants.Tables.CarPoolOpportunityTable.Columns.Origin).AsString(int.MaxValue).NotNullable()
                .WithColumn(DbConstants.Tables.CarPoolOpportunityTable.Columns.DaysAvailable).AsInt32().NotNullable()
                .WithColumn(DbConstants.Tables.CarPoolOpportunityTable.Columns.Destination).AsString(int.MaxValue).NotNullable()
                .WithColumn(DbConstants.Tables.CarPoolOpportunityTable.Columns.AvailableSeats).AsInt32().NotNullable()
                .WithColumn(DbConstants.Tables.CarPoolOpportunityTable.Columns.Notes).AsString(int.MaxValue).Nullable()
                .WithColumn(DbConstants.Tables.CarPoolOpportunityTable.Columns.OwnerOrLeader).AsString(int.MaxValue).NotNullable()
                .WithColumn(DbConstants.Tables.CarPoolOpportunityTable.Columns.DateCreated).AsDateTime().NotNullable();

        }

        public override void Down()
        {
        }
    }
}