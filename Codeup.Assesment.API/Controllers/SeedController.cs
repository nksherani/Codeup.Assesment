using Codeup.Assesment.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Codeup.Assesment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly ISeedService _seedService;

        public SeedController(ISeedService seedService)
        {
            this._seedService = seedService;
        }
        // GET: api/<SeedController>
        [HttpGet]
        public async Task<IActionResult> Seed()
        {
            await _seedService.SeedAsync();
            return Ok();
        }

        
    }
}
