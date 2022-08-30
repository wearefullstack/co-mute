using System.Data;
using ansyl.dao;

namespace CoMute.Lib
{
    public class DaoFactory
    {
        public static IDbConnection OneTaskConnection()
        {
            return new DataConnector().GetConnection();
        }
    }

    /// <summary>
    /// BaseUnitOfWork is a class which I previously wrote to implement the Unit of Work pattern
    /// </summary>
    public sealed class UnitOfWork : BaseUnitOfWork
    {
        public UnitOfWork() : base(DaoFactory.OneTaskConnection(), MySqlStatement.Instance)
        {
        }
    }

    /// <summary>
    /// BaseTransaction is a class which I previously wrote to implement a Transaction
    /// based on a Unit of Work and Repository patterns
    /// </summary>
    public sealed class OneTransaction : BaseTransaction<UnitOfWork>
    {
    }

    /// <summary>
    /// BaseOneTask is a class which I previously wrote to implement a special case
    /// whereby OneTransaction has just ONE TASK to perform.
    /// It shortens the amount of code required to be written
    /// </summary>
    public sealed class OneTask<TEntity> : BaseOneTask<UnitOfWork, TEntity> where TEntity : IDaoObject
    {
    }
}
