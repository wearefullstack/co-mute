using Microsoft.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Co_Mute.Api.Repository;
using Co_Mute.Api.Models.Dto;
using Microsoft.AspNetCore.Authorization;

namespace Co_Mute.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class CarPoolTicketsController : ControllerBase
    {
        private readonly ICarPoolTicketRepository _iCarPoolTicketRepository;

        public CarPoolTicketsController(ICarPoolTicketRepository iCarPoolTicketRepository) => _iCarPoolTicketRepository = iCarPoolTicketRepository;

        [HttpPost("{id}")]
        public async Task<IActionResult> FindCarPoolTickets([FromBody] SearchCarPoolTicketsDto searchCarPoolTicketsDto, int id)
        {
            if (string.IsNullOrEmpty(searchCarPoolTicketsDto.SearchText))
            {
                BadRequest(new { message = "cant have empty search field" });
            }
            try
            {

                var carPoolTickets = await _iCarPoolTicketRepository.FindCarPoolTickets(searchCarPoolTicketsDto, id);
            
                if (carPoolTickets is not null)
                {
                    return Ok(carPoolTickets);
                }
                return NotFound("There are no records found");
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCreatedCarPoolTicketsByUserId(int id)
        {
            try
            {
                var carPoolTickets = await _iCarPoolTicketRepository.GetCreatedCarPoolTicketsByUserId(id);

                if (carPoolTickets is not null)
                {
                    return Ok(carPoolTickets);
                }
                return NotFound("There are no records found");
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegisteredCarPoolTicketsByUserId(int id)
        {
            try { 
                var carPoolTickets = await _iCarPoolTicketRepository.GetRegisteredCarPoolTicketsByUserId(id);

                if (carPoolTickets is not null)
                {
                    return Ok(carPoolTickets);
                }
                return NotFound("There are no records found");
            }
            catch(Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> CreateCarPoolTicket([FromBody] CreateCarPoolTicket oCreateCarPoolTicket, int id)
        {
            try
            {

                int createdId = await _iCarPoolTicketRepository.CreateCarPoolTicket(oCreateCarPoolTicket, id);
                if (createdId > 0)
                    return Ok(createdId);
                return BadRequest(new { message = "overlapping times for car pool ticket creation not allowed"});
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCarPoolTicketDetails([FromBody] UpdateCarPoolTicketDetailsDto updateCarPoolTicketDetailsDto, int id)
        {
            try
            {
                int updatedCarPoolTicketId = await _iCarPoolTicketRepository.UpdateCarPoolTicketDetails(updateCarPoolTicketDetailsDto, id);
                if (updatedCarPoolTicketId < 0)
                {
                    return BadRequest(new { message = "This ticket you want to update does not exsist" });
                }
                return Ok(updatedCarPoolTicketId);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> CancelCreatedCarPoolTicketID([FromBody] FunctionCommandUser functionCommandUserDto, int id)
        {
            try
            {
                int cancelledTicketStatuts = await _iCarPoolTicketRepository.CancelCreatedCarPoolTicketID(functionCommandUserDto, id);
                if (cancelledTicketStatuts < 0)
                {
                    return BadRequest(new { message = "This Ticket can not be cancelled without ownership" });
                }
                if (cancelledTicketStatuts == 0)
                {
                    return BadRequest(new { message = "This Ticket is already cancelled" });
                }
                return Ok(cancelledTicketStatuts);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("{id}")]
        public async Task<ActionResult> JoinCarPoolTicket([FromBody] FunctionCommandCarPoolTicketDto oFunctionCommandCarPoolTicketDto, int id)
        {
            try
            {
                int joinedCarPoolTicketId = await _iCarPoolTicketRepository.JoinCarPoolTicket(oFunctionCommandCarPoolTicketDto, id);
                if (joinedCarPoolTicketId == 0)
                    return NotFound(new { message = "This Ticket could not be found" });
                if (joinedCarPoolTicketId == -2)
                    return BadRequest(new { message = "Cant join when you have a trip acitve during these times" });
                if (joinedCarPoolTicketId == -1)
                    return BadRequest(new { message = "Cant join expired or fully booked trip" });
                return Ok(new { Response = joinedCarPoolTicketId });
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPost("{id}")]
        public async Task<ActionResult> CancelJoinCarPoolTicket([FromBody] FunctionCommandCarPoolTicketDto oFunctionCommandCarPoolTicketDto, int id)
        {
            try
            {
                int cancelledJoinCarPoolTicketId = await _iCarPoolTicketRepository.CancelJoinCarPoolTicket(oFunctionCommandCarPoolTicketDto, id);
                if (cancelledJoinCarPoolTicketId > 0)
                {
                    return Ok(cancelledJoinCarPoolTicketId);
                }
                return NotFound("There are no records found for carppol ticket id");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]

        public async Task<IActionResult> GetCarPoolTicketDetailsById(int id)
        {
            try
            {
                var carPoolTicketDetails = await _iCarPoolTicketRepository.GetCarPoolTicketDetailsById(id);

                if (carPoolTicketDetails is not null)
                {
                    return Ok(carPoolTicketDetails);
                }
                return NotFound("There are no records found");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
