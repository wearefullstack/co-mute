using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using CoMute.BL;
using CoMute.Web.Models.Dto;

namespace CoMute.Web.Controllers.API
{
    public class JoinLeaveCarPoolController : ApiController
    {
        HttpResponseMessage response;
        private JoinLeaveCarPoolLogic _joinLeaveCarPoolLogic = new JoinLeaveCarPoolLogic();


        //TODO 
        //private IMapper _mapper;

        ///// <summary>
        ///// CarPoolController Non-Default Constructor 
        ///// </summary>
        ///// <param name="Mapper">Mapper object injected through Unity, providing object mapping functionality</param> 
        //public JoinLeaveCarPoolController(IMapper Mapper,IJoinLeaveCarPool JoinLeaveCarPool)
        //{
        //   _ioinLeaveCarPool=JoinLeaveCarPool;
        //   _mapper = Mapper;
        //}
        public JoinLeaveCarPoolController()
        {

        }

        ///<summary>  
        /// This method is used to get Get All Users Joined the Car Pool
        ///</summary>  
        ///<returns></returns>  
        [HttpGet]
        public IEnumerable<BE.JoinLeaveCarPool> GetAllCarPools()
        {
            var joinLeaveCarPoolList = _joinLeaveCarPoolLogic.GetAllJoinedCarPool();
            try
            {


                if (!object.Equals(joinLeaveCarPoolList, null))
                {
                    response = Request.CreateResponse<List<BE.JoinLeaveCarPool>>(HttpStatusCode.OK, joinLeaveCarPoolList);
                }
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Status = 0;
                result.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
            return (IEnumerable<BE.JoinLeaveCarPool>)joinLeaveCarPoolList.ToList();
        }

        /// <summary>
        /// Deactivate/Activate the user from Car Pool
        /// </summary>      
        [HttpPost]
        //[CustomAuthorize("CarPool")]
        [AllowAnonymous]
        public IHttpActionResult ChangeStatusJoinLeaveCarPool(int JoinLeaveCarPoolId, bool IsActive)
        {
            var checkStatus = _joinLeaveCarPoolLogic.ChangeStatusJoinLeaveCarPool(JoinLeaveCarPoolId, IsActive);
            return Ok("User successfully changed status to" + checkStatus);

        }

        /// <summary>
        /// Join Car Pool Method for users
        /// </summary>   
        [HttpPost]
        public async Task<HttpResponseMessage> JoinCarPool(BE.JoinLeaveCarPool joinLeaveCarPool)
        {      

            _joinLeaveCarPoolLogic.AddJoinLeaveCarPool(joinLeaveCarPool);
            return Request.CreateResponse(HttpStatusCode.Created);
        }

        /// <summary>
        /// Leave/Delete the user from the Car Pool Permanent
        /// </summary>   
        [HttpPost]
        public async Task<HttpResponseMessage> LeaveCarPool(BE.JoinLeaveCarPool joinLeaveCarPool)
        {
            _joinLeaveCarPoolLogic.DeleteJoinLeaveCar(joinLeaveCarPool);
            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
