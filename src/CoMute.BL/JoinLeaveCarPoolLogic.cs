using CoMute.BE;
using CoMute.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.BL
{
    public class JoinLeaveCarPoolLogic: IJoinLeaveCarPool
    {
        JoinLeaveCarPoolRepository obj = new JoinLeaveCarPoolRepository();
        //TODO 
        //private readonly IUnitOfWork unitOfWork;
        //private GenericRepository<JoinLeaveCarPool> joinLeaveCarPoolRepository;

        //public CarPoolLogic(IUnitOfWork unitOfWork)
        //{
        //    this.unitOfWork = unitOfWork;
        //    this.joinLeaveCarPoolRepository = unitOfWork.GetRepository<JoinLeaveCarPool>();
        //}

        #region Read
        //Method to activate/deactivate the user from the CarPool
        public bool ChangeStatusJoinLeaveCarPool(int JoinLeaveCarPoolId, bool IsActive)
        {
            var getJoinLeaveCarPool = obj.GetEntity(JoinLeaveCarPoolId);
            getJoinLeaveCarPool.IsActive = IsActive;
            obj.Update(getJoinLeaveCarPool);

            return IsActive;

        }

        // view all the car-pool opportunities Joined
        public List<JoinLeaveCarPool> GetAllJoinedCarPool()
        {


            var JoinedCarPool = obj.SelectAll().ToList();


            return JoinedCarPool;
        }
        #endregion

        #region Modify
        //This is the Add method using JoinLeaveCarPoolRepository obj to call Insert method on the data layer,add new JoinLeaveCarPool to the database
        //Join Car-pool Opportunity

        public void AddJoinLeaveCarPool(JoinLeaveCarPool joinLeaveCarPool)
        {
            obj.Insert(joinLeaveCarPool);

        }

        //This is the Update method using JoinLeaveCarPoolRepository obj to call update method on the data layer,modifies JoinLeaveCarPool info from the database
        public void UpdateJoinLeaveCar(JoinLeaveCarPool joinLeaveCarPool)
        {
            obj.Update(joinLeaveCarPool);
        }

        //This is the Delete method using JoinLeaveCarPoolRepository obj to call delete method on the data layer. It deletes the JoinLeaveCarPool info from the database
        //Leave Car-pool Opportunity  permanently or set active status to false for temporary using ChangeStatusJoinLeaveCarPool method
        public void DeleteJoinLeaveCar(JoinLeaveCarPool joinLeaveCarPool)
        {
            obj.Delete(joinLeaveCarPool);
        }
        #endregion Modify

    }
}
