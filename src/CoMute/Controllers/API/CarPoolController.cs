using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CoMute.BL;
using CoMute.BE;
using System.ComponentModel;
using System.Threading.Tasks;
using CoMute.Web.Models.Dto;

namespace CoMute.Web.Controllers.API
{
    public class CarPoolController : ApiController
    {
   
        HttpResponseMessage response;
        private CarPoolLogic _carPoolLogic =new CarPoolLogic();


        //TODO
        //private IMapper _mapper;

        ///// <summary>
        ///// CarPoolController Non-Default Constructor 
        ///// </summary>
        ///// <param name="Mapper">Mapper object injected through Unity, providing object mapping functionality</param> 
        //public CarPoolController(IMapper Mapper,ICarPoolLogic CarPoolLogic)
        //{
        //   _carPoolLogic=CarPoolLogic;
        //   _mapper = Mapper;
        //}
        public CarPoolController()
        {
          
        }

        ///<summary>  
        /// This method is used to get Get All Car Pools  
        ///</summary>  
        ///<returns></returns>  
        [HttpGet]
        public IEnumerable<BE.CarPool> GetAllCarPools()
        {
            var carPoolList = _carPoolLogic.GetAllCarPools();
            try
            {


                if (!object.Equals(carPoolList, null))
                {
                    response = Request.CreateResponse<List<BE.CarPool>>(HttpStatusCode.OK, carPoolList);
                }
            }
            catch (Exception ex)
            {
                var result = new Result();
                result.Status = 0;
                result.Message = ex.Message;
                response = Request.CreateResponse(HttpStatusCode.InternalServerError, result);
            }
            return (IEnumerable<BE.CarPool>)carPoolList.ToList();
        }

        /// <summary>
        /// Gets all CarPools for a specific User
        /// </summary>      


        [HttpGet]
        [Route("GetCarPoolsByUserId/{UserId:int}")]
        //[CustomAuthorize("CarPool")]
        [AllowAnonymous]
        public IHttpActionResult GetCarPoolsByUserId(int UserId)
        {
            var carPoolList = _carPoolLogic.GetCarPoolByUserId(UserId);
            return Ok(carPoolList);

        }

        /// <summary>
        /// Add CarPool
        /// </summary>   
        [HttpPost]
        public async Task<HttpResponseMessage> AddCarPool(BE.CarPool carPool)
        {                 
             _carPoolLogic.AddCarPool(carPool);
            return Request.CreateResponse(HttpStatusCode.Created);
        }
    }
}
