using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using co_mute_be.Database;
using co_mute_be.Models;
using co_mute_be.Abstractions.Models;

namespace co_mute_be.Controllers
{
    [Route("api/car-pool")]
    [ApiController]
    public class CarPoolOppsController : ControllerBase
    {
        private readonly DataContext _context;

        public CarPoolOppsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/car-pool
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CarPoolOpp>>> GetCarPoolOpps()
        {
          if (_context.CarPoolOpps == null)
          {
              return NotFound();
          }
            return await _context.CarPoolOpps.ToListAsync();
        }

        // GET: api/car-pool/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CarPoolOpp>> GetCarPoolOpp(string id)
        {
          if (_context.CarPoolOpps == null)
          {
              return NotFound();
          }
            var carPoolOpp = await _context.CarPoolOpps.FindAsync(id);

            if (carPoolOpp == null)
            {
                return NotFound();
            }

            return carPoolOpp;
        }

        // PUT: api/car-pool/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarPoolOpp(string id, CarPoolOpp carPoolOpp)
        {
            if (id != carPoolOpp.CarPoolOppId)
            {
                return BadRequest();
            }

            _context.Entry(carPoolOpp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarPoolOppExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/car-pool
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<CarPoolOpp>>> PostCarPoolOpp(CreateCarPoolOppDto carPoolOppDto)
        {
            try
            {
                if (_context.CarPoolOpps == null)
                {
                    return Problem("Entity set 'DataContext.CarPoolOpps'  is null.");
                }
                var user = await _context.Users.SingleOrDefaultAsync(user => user.UserId.Equals(carPoolOppDto.UserId));

                if (user == null)
                {
                    return Problem("No user found");
                }

                if (!IsValidOppDate(carPoolOppDto, user.CarPoolOpps))
                {
                    return BadRequest(new ApiResult<User>
                    {
                        Success = false,
                        Error = "Invalid booking slot"
                    });
                }

                var carPoolOpp = new CarPoolOpp
                {
                    Depart = carPoolOppDto.Depart,
                    Arrive = carPoolOppDto.Arrive,
                    Origin = carPoolOppDto.Origin,
                    AvailableSeats = carPoolOppDto.AvailableSeats,
                    Destination = carPoolOppDto.Destination,
                    Notes = carPoolOppDto.Notes
                };

                user.CarPoolOpps.Add(carPoolOpp);

                await _context.SaveChangesAsync();

                return Ok(new ApiResult<CarPoolOpp>
                {
                    Success = true,
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResult<CarPoolOpp>
                {
                    Success = false,
                    Error = ex.Message
                });
            }

        }

        // DELETE: api/car-pool/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarPoolOpp(string id)
        {
            if (_context.CarPoolOpps == null)
            {
                return NotFound();
            }
            var carPoolOpp = await _context.CarPoolOpps.FindAsync(id);
            if (carPoolOpp == null)
            {
                return NotFound();
            }

            _context.CarPoolOpps.Remove(carPoolOpp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarPoolOppExists(string id)
        {
            return (_context.CarPoolOpps?.Any(e => e.CarPoolOppId == id)).GetValueOrDefault();
        }

        private bool IsValidOppDate(CreateCarPoolOppDto dto, List<CarPoolOpp> opps)
        {
            if(opps == null || opps.Count == 0)
            {
                return true;
            }

            var isValid = true;

            opps.ForEach(x =>
            {
                if (dto.Depart >= x.Depart && dto.Depart <= x.Arrive)
                {
                    isValid = false;
                }

                if (dto.Depart <= x.Depart && (dto.Arrive >= x.Depart && dto.Arrive <= x.Arrive))
                {
                    isValid = false;
                }
            });
            return isValid;
        }
    }
}
