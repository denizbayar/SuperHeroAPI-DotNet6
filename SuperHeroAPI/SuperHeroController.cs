using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SuperHeroAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        public static List<SuperHero> heroes = new List<SuperHero>
            {
                new SuperHero{ 
                    Id = 1, 
                    Name =  "Spiderman",
                    FirstName="Peter", 
                    LastName="Parker", 
                    Place="New York City"
                },
                new SuperHero{
                    Id = 2,
                    Name =  "Ironman",
                    FirstName="Tony",
                    LastName="Stark",
                    Place="Long Island"
                }
            };
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> Get()
        {
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Get(int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null) return BadRequest("Hero not found!");
            return Ok(hero);
        }

        [HttpPost]
        public async Task<ActionResult<List<SuperHero>>> AddHero (SuperHero hero)
        {
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpPut]
        public async Task<ActionResult<List<SuperHero>>> UpdateHero (SuperHero request)
        {
            var foundHero = await _context.SuperHeroes.FindAsync(request.Id);
            if (foundHero == null) return BadRequest("Hero not found!");

            foundHero.Name = request.Name;
            foundHero.FirstName = request.FirstName;
            foundHero.LastName = request.LastName;
            foundHero.Place = request.Place;

            await _context.SaveChangesAsync();
                
            return Ok(await _context.SuperHeroes.ToListAsync());
        }

        [HttpDelete("{id}")] 
        public async Task<ActionResult<List<SuperHero>>> DeleteHero (int id)
        {
            var hero = await _context.SuperHeroes.FindAsync(id);
            if (hero == null) return BadRequest("Wrong id!");
            _context.SuperHeroes.Remove(hero);

            await _context.SaveChangesAsync();
            return Ok(await _context.SuperHeroes.ToListAsync());
        }
    }
}
