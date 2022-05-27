using BlazorApp2.Server.Database;
using BlazorApp2.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp2.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        //public static List<Comic> comics = new List<Comic>()
        //{
        //    new Comic{Id=1, Name="Marvel"},
        //    new Comic{Id=2, Name="DC"},
        //};

        //public static List<SuperHero> heroes = new List<SuperHero>()
        //{
        //    new SuperHero {
        //        Id=1,
        //        FirstName="Peter",
        //        LastName="Parker",
        //        HeroName="Spiderman",
        //        Comic = comics[0],
        //        ComicId=1
        //    },

        //    new SuperHero {
        //        Id=2,
        //        FirstName="Bruce",
        //        LastName="Wayne",
        //        HeroName="Batman",
        //        Comic = comics[1],
        //        ComicId=2
        //    },
        //};

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeroes()
        {
            var heroes = await _context.SuperHeroes.ToListAsync();
            return Ok(heroes);
        }

        [HttpGet("comics")]
        public async Task<ActionResult<List<Comic>>> GetComics()
        {
            var comics = await _context.Comics.ToListAsync();
            return Ok(comics);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetSingleSuperHeroe(int id)
        {
            var hero = await _context.SuperHeroes
                .Include(h => h.Comic)
                .FirstOrDefaultAsync(h => h.Id == id);
            if (hero == null)
            {
                return NotFound("Sorry no hero here!");
            }
            return Ok(hero);
        }

        [HttpPost] // Uses for Create
        public async Task<ActionResult<List<SuperHero>>> CreateSuperHero(SuperHero hero)
        {
            hero.Comic = null;
            _context.SuperHeroes.Add(hero);
            await _context.SaveChangesAsync();

            return Ok(await getDBHeroes());
        }

        private async Task<List<SuperHero>> getDBHeroes()
        {
            return await _context.SuperHeroes.Include(h => h.Comic).ToListAsync();
        }

        [HttpPut] // Uses for Update
        public async Task<ActionResult<List<SuperHero>>> UpdateSuperHero(SuperHero hero, int id)
        {
            var dbHero = await _context.SuperHeroes
                .Include(sh => sh.Comic)
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (dbHero == null)
            {
                return NotFound("Sorry no hero here!");
            }
            dbHero.FirstName = hero.FirstName;
            dbHero.LastName = hero.LastName;
            dbHero.HeroName = hero.HeroName;
            dbHero.ComicId = hero.ComicId;

            await _context.SaveChangesAsync();

            return Ok(await getDBHeroes());
        }


        [HttpDelete("{id}")] // Used for Delete
        public async Task<ActionResult<List<SuperHero>>> DeleteSuperHero(int id)
        {
            var dbHero = await _context.SuperHeroes
                .Include(sh => sh.Comic)
                .FirstOrDefaultAsync(sh => sh.Id == id);
            if (dbHero == null)
            {
                return NotFound("Sorry no hero here!");
            }

            _context.SuperHeroes.Remove(dbHero);

            await _context.SaveChangesAsync();

            return Ok(await getDBHeroes());
        }
    }
}
