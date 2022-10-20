using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.DL
{
    public abstract class BaseRepository<TContext> : IDisposable
     where TContext : System.Data.Linq.DataContext
    {
        protected TContext db;

        public BaseRepository()
        {
            Type type = typeof(TContext);

            this.db = (TContext)Activator.CreateInstance(typeof(TContext), new object[] { "Data Source=LAPTOP-FQ53KOIK\\SQL_2019;Initial Catalog=CoMuteVersion;User Id=sa;Password=1234;Integrated Security=True" });
        }

        public void SubmitChanges()
        {
            try
            {
                this.db.SubmitChanges(ConflictMode.FailOnFirstConflict);
            }
            catch (ChangeConflictException e)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.AppendLine(string.Format("Optimistic concurrency error: {0}", e.Message));

                foreach (ObjectChangeConflict occ in db.ChangeConflicts)
                {
                    MetaTable metatable = db.Mapping.GetTable(occ.Object.GetType());
                    var entityInConflict = occ.Object;
                    sb.AppendLine(string.Format("Table name: {0}", metatable.TableName));
                    sb.AppendLine(string.Format("Primary Key: {0}", "Unknown"));
                    foreach (MemberChangeConflict mcc in occ.MemberConflicts)
                    {
                        object currVal = mcc.CurrentValue;
                        object origVal = mcc.OriginalValue;
                        object databaseVal = mcc.DatabaseValue;
                        MemberInfo mi = mcc.Member;
                        sb.AppendLine(string.Format("Member: {0}", mi.Name));
                        sb.AppendLine(string.Format("current value: {0}", currVal));
                        sb.AppendLine(string.Format("original value: {0}", origVal));
                        sb.AppendLine(string.Format("database value: {0}", databaseVal));
                    }
                }

                throw new DBConcurrencyException(sb.ToString());
            }
        }


        #region IDisposable Members
        void IDisposable.Dispose()
        {
            this.db.Dispose();
        }
        #endregion
    }
}
