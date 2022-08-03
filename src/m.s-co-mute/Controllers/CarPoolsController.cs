using m.s_co_mute.Data;
using m.s_co_mute.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace m.s_co_mute.Controllers
{
    public class CarPoolsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CarPoolsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CarPools
        public async Task<IActionResult> Index()
        {
            return _context.CarPool != null ?
                        View(await _context.CarPool.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.CarPool'  is null.");
        }

        // GET: CarPools/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return View();
        }

        // GET: CarPools/ShowCreatedCarPool
        public async Task<IActionResult> ShowCreatedCarPool()
        {
            //return View("Index", await _context.CarPool.Where(j => j.Carpool.Contains
            //(SignedInUserName)).ToListAsync());
            return View();
        }

        // POST: CarPools/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return View("Index", await _context.CarPool.Where(j => j.Destination.Contains
            (SearchPhrase)).ToListAsync());
        }

        // GET: CarPools/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CarPool == null)
            {
                return NotFound();
            }

            var carPool = await _context.CarPool
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carPool == null)
            {
                return NotFound();
            }

            return View(carPool);
        }

        // GET: CarPools/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarPools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ExpectedArrivalTime,Origin,DaysAvailable,Destination,AvailableSeats,Owner,Notes")] CarPool carPool)
        {
            if (ModelState.IsValid)
            {
                _context.Add(carPool);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carPool);
        }

        // GET: CarPools/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CarPool == null)
            {
                return NotFound();
            }

            var carPool = await _context.CarPool.FindAsync(id);
            if (carPool == null)
            {
                return NotFound();
            }
            return View(carPool);
        }

        // POST: CarPools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ExpectedArrivalTime,Origin,DaysAvailable,Destination,AvailableSeats,Owner,Notes")] CarPool carPool)
        {
            if (id != carPool.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(carPool);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarPoolExists(carPool.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(carPool);
        }

        // GET: CarPools/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CarPool == null)
            {
                return NotFound();
            }

            var carPool = await _context.CarPool
                .FirstOrDefaultAsync(m => m.Id == id);
            if (carPool == null)
            {
                return NotFound();
            }

            return View(carPool);
        }

        // POST: CarPools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CarPool == null)
            {
                return Problem("Entity set 'ApplicationDbContext.CarPool'  is null.");
            }
            var carPool = await _context.CarPool.FindAsync(id);
            if (carPool != null)
            {
                _context.CarPool.Remove(carPool);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarPoolExists(int id)
        {
            return (_context.CarPool?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
