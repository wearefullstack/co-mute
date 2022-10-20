using CoMute.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.BL
{
    public interface IJoinLeaveCarPool
    {
        #region Read
        bool ChangeStatusJoinLeaveCarPool(int JoinLeaveCarPoolId, bool IsActive);
        List<JoinLeaveCarPool> GetAllJoinedCarPool();

        #endregion

        #region Modify
        void AddJoinLeaveCarPool(JoinLeaveCarPool joinLeaveCarPool);
        void UpdateJoinLeaveCar(JoinLeaveCarPool joinLeaveCarPool);
        void DeleteJoinLeaveCar(JoinLeaveCarPool joinLeaveCarPool);
        #endregion
    }
}
