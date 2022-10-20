using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.DL
{
    /// <summary>
    /// Contains functions to manipulate EF transactions
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Gets the repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <returns>Repository</returns>
        ///TODO
        //GenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;

        /// <summary>
        /// Saves all pending changes
        /// </summary>
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        void Save();

        #region CarPoolRepository
        CarPoolRepository GetCarPoolRepository();
        #endregion

        #region UserRepository
        UserRepository GetUserRepository();
        #endregion

        #region JoinLeaveCarPoolRepository

        JoinLeaveCarPoolRepository GetJoinLeaveCarPoolRepository();

       

        #endregion

       
    }
}
