using CoMute.Core.Domain;
using System.Data.Entity.ModelConfiguration;
using UserTable = CoMute.DB.DbConstants.Tables.UserTable;

namespace CoMute.DB.Mappings
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(it => it.UserId);

            ToTable(UserTable.TableName);
            Property(p => p.UserId).HasColumnName(UserTable.Columns.UserId);
            Property(p => p.Name).HasColumnName(UserTable.Columns.Name);
            Property(p => p.Surname).HasColumnName(UserTable.Columns.Surname);
            Property(p => p.EmailAddress).HasColumnName(UserTable.Columns.EmailAddress);
            Property(p => p.PhoneNumber).HasColumnName(UserTable.Columns.PhoneNumber);
            Property(p => p.PasswordHash).HasColumnName(UserTable.Columns.PasswordHash);
            Property(p => p.PasswordSalt).HasColumnName(UserTable.Columns.PasswordSalt);
        }
    }
}
