
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using co_mute_be.Database;
using co_mute_be.Models;
using co_mute_be.Models.Dto;
using co_mute_be.Abstractions.Utils;
using co_mute_be.Abstractions.Models;

namespace co_mute_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetCarPoolOpps()
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(string id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = _context.Users.Where(x => x.UserId.Equals(id)).FirstOrDefault();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(string id, User user)
        {
            if (!id.Equals(user.UserId))
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResult<User>>> CreateUser(CreateUserDto userDto)
        {
            try
            {
                if (_context.Users == null)
                {
                    return Problem($"Entity set 'DataContext.Users' is null.");
                }

                var user = Models.User.FromDto(userDto);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return Ok(new ApiResult<User>
                {
                    Success = true,
                    Result = user
                });
            }
            catch(Exception ex)
            {
                return BadRequest(new ApiResult<User>
                {
                    Success = true,
                    Error = ex.Message
                });
            }

        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (_context.Users == null)
            {
                return NotFound();
            }
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(string id)
        {
            return (_context.Users?.Any(e => e.UserId.Equals(id))).GetValueOrDefault();
        }
    
    }
}
