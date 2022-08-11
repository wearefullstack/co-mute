using CoMute.Core.Domain;
using System.Data.Entity.ModelConfiguration;
using JoinCarPoolOpportunityTable = CoMute.DB.DbConstants.Tables.JoinCarPoolOpportunityTable;

namespace CoMute.DB.Mappings
{
    public class JoinCarPoolsOpportunityMap : EntityTypeConfiguration<JoinCarPoolsOpportunity>
    {
        public JoinCarPoolsOpportunityMap()
        {
            HasKey(it => it.JoinCarPoolsOpportunityId);

            ToTable(JoinCarPoolOpportunityTable.TableName);
            Property(p => p.JoinCarPoolsOpportunityId).HasColumnName(JoinCarPoolOpportunityTable.Columns.JoinCarPoolsOpportunityId);
            Property(p => p.CarPoolId).HasColumnName(JoinCarPoolOpportunityTable.Columns.CarPoolId);
            Property(p => p.UserId).HasColumnName(JoinCarPoolOpportunityTable.Columns.UserId);
            Property(p => p.DateJoined).HasColumnName(JoinCarPoolOpportunityTable.Columns.DateJoined);
        }
    }
}
