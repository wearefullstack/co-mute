using FSWebApi.Authorization;
using FSWebApi.Dto.Carpool;
using FSWebApi.Interfaces;
using FSWebApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FSWebApi.Controllers
{
    [Route("api/v1/Carpools")]
    [ApiController]
    public class CarpoolController : ControllerBase
    {
        private readonly ICarpoolService _carpoolService;

        public CarpoolController(ICarpoolService carpoolService)
        {
            _carpoolService = carpoolService;
        }

        //TODO: Get userId from JWT claims and not from the request body
        //to make sure userId used to join/create carpools matches the logged on user


        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<CarpoolDTO>> GetAll()
        {
            return Ok(_carpoolService.GetAllCarpools());
        }

        [Authorize]
        [HttpGet("joined/{userId}")]
        public ActionResult<IEnumerable<CarpoolDTO>> GetJoinedCarpools(Guid userId)
        {
            return Ok(_carpoolService.GetJoinedCarpools(userId));
        }

        [Authorize]
        [HttpGet("created/{userId}")]
        public ActionResult<IEnumerable<CarpoolDTO>> GetCreatedCarpools(Guid userId)
        {
            return Ok(_carpoolService.GetCreatedCarpools(userId));
        }

        [Authorize]
        [HttpPost("register")]
        public ActionResult<CarpoolDTO> RegisterCarpool(CreateCarpoolDTO carpool)
        {
   
            var createdCarpool = _carpoolService.RegisterCarpool(carpool);
            return Created(createdCarpool.CarpoolId.ToString(), createdCarpool);
        }

        [Authorize]
        [Route("join")]
        [HttpPost]
        public ActionResult<CarpoolDTO> JoinCarpool(JoinCarpoolDTO joinCarpoolDTO)
        {
            var carpool = _carpoolService.JoinCarpool(joinCarpoolDTO);
            return Ok(carpool);
        }

        //It us not advised to have a request body in a HttpDelete
        //TODO: Get UserId from JWT claims
        [Authorize]
        [HttpDelete("Exit/{carpoolId}/{userId}")]
        public ActionResult<bool> ExitCarpool(Guid carpoolId, Guid userId)
        {
            bool success = _carpoolService.ExitCarpool(carpoolId, userId);
            return Ok(success);

        }

    }
}
