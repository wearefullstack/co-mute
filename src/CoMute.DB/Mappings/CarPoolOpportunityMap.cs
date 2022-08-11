using CoMute.Core.Domain;
using System.Data.Entity.ModelConfiguration;
using CarPoolOpportunityTable = CoMute.DB.DbConstants.Tables.CarPoolOpportunityTable;

namespace CoMute.DB.Mappings
{
    public class CarPoolOpportunityMap : EntityTypeConfiguration<CarPoolOpportunity>
    {
        public CarPoolOpportunityMap()
        {
            HasKey(it => it.CarPoolId);

            ToTable(CarPoolOpportunityTable.TableName);
            Property(p => p.CarPoolId).HasColumnName(CarPoolOpportunityTable.Columns.CarPoolId);
            Property(p => p.DepartureTime).HasColumnName(CarPoolOpportunityTable.Columns.DepartureTime);
            Property(p => p.ExpectedArrivalTime).HasColumnName(CarPoolOpportunityTable.Columns.ExpectedArrivalTime);
            Property(p => p.Origin).HasColumnName(CarPoolOpportunityTable.Columns.Origin);
            Property(p => p.DaysAvailable).HasColumnName(CarPoolOpportunityTable.Columns.DaysAvailable);
            Property(p => p.Destination).HasColumnName(CarPoolOpportunityTable.Columns.Destination);
            Property(p => p.AvailableSeats).HasColumnName(CarPoolOpportunityTable.Columns.AvailableSeats);
            Property(p => p.Notes).HasColumnName(CarPoolOpportunityTable.Columns.Notes);
            Property(p => p.OwnerOrLeader).HasColumnName(CarPoolOpportunityTable.Columns.OwnerOrLeader);
            Property(p => p.DateCreated).HasColumnName(CarPoolOpportunityTable.Columns.DateCreated);
        }
    }
}
