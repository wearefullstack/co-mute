using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoMuteCore.Models;
using CoMuteCore.Services;
using Microsoft.AspNetCore.Mvc;

namespace CoMuteCore.Controllers.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarPoolController : ControllerBase
    {
        public readonly ICarPoolService _carPoolService;

        public CarPoolController(ICarPoolService carPoolService)
        {
            _carPoolService = carPoolService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var carPools = await _carPoolService.GetAllCarPoolsAsync();

            return Ok(carPools);
        }

        public async Task<IActionResult> GetCarPool(int id)
        {
            var carPool = await _carPoolService.ViewCarPoolAsync(id);
            return Ok(carPool);
        }

        public async Task<IActionResult> Create(CarPool carPool)
        {
            await _carPoolService.CreateCarPoolAsync(carPool);
            return Ok();
        }

    }
}