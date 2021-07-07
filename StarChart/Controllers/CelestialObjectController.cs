using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StarChart.Data;

namespace StarChart.Controllers
{
    [Route("")]
    [ApiController]
    public class CelestialObjectController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CelestialObjectController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{id:int}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var item = _context.CelestialObjects.Find(id);

            if (item is null)
                return NotFound();

            item.Satellites = _context.CelestialObjects
                .Where(x => x.OrbitedObjectId == item.Id).ToList();

            return Ok(item);
        }

        [HttpGet("{name}")]
        public IActionResult GetByName(string name)
        {
            var item = _context.CelestialObjects.Single(x => x.Name == name);

            if (item is null)
                return NotFound();

            item.Satellites = _context.CelestialObjects
                .Where(x => x.OrbitedObjectId == item.Id).ToList();

            return Ok(item);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_context.CelestialObjects.Include("Satellites").ToList());
        }

    }
}
