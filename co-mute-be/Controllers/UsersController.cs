using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using co_mute_be.Database;
using co_mute_be.Models;
using co_mute_be.Models.Dto;

namespace co_mute_be.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetCarPoolOpps()
        {
            if (_context.CarPoolOpps == null)
            {
                return NotFound();
            }
            return await _context.CarPoolOpps.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(long id)
        {
            if (_context.CarPoolOpps == null)
            {
                return NotFound();
            }
            var user = await _context.CarPoolOpps.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, User user)
        {
            if (id != user.Id)
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
        public async Task<ActionResult<User>> CreateUser(CreateUserDto userDto)
        {
            if (_context.CarPoolOpps == null)
            {
                return Problem("Entity set 'UserContext.CarPoolOpps' is null.");
            }

            var user = new User();
            user.Id = Guid.NewGuid();
            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.Email = userDto.Email;
            user.Phone = userDto.Phone;
            user.Password = userDto.Password;
            user.PasswordHash = GetPasswordHash(userDto.Password);

            _context.CarPoolOpps.Add(user);
            await _context.SaveChangesAsync();
            var list = await _context.CarPoolOpps.ToListAsync();
    
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(long id)
        {
            if (_context.CarPoolOpps == null)
            {
                return NotFound();
            }
            var user = await _context.CarPoolOpps.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.CarPoolOpps.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(Guid id)
        {
            return (_context.CarPoolOpps?.Any(e => e.Id.Equals(id))).GetValueOrDefault();
        }

        private string GetPasswordHash(string password)
        {
            return $"###{password}###"; // Super secret hash algo here...
        }
    }
}
