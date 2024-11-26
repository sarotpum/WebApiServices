using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedService.DBContext;
using SharedService.Models.Tlous;
using SharedService.Models.Tlous.DTOs;
using System.Linq;

namespace WebApiServices.Controllers.TlouController
{
    [ApiController]
    [Route("api/[controller]")]
    public class TlouController : Controller
    {
        private readonly DatasContext _datasContext;

        public TlouController(DatasContext datasContext)
        {
            _datasContext = datasContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterModel>> GetCharacterById(int id)
        {
            var character = await _datasContext.Characters
            .Include(c => c.Backpack)
            .Include(c => c.Weapons)
            .Include(c => c.Factions)
            .FirstOrDefaultAsync(c => c.Id == id);

            return Ok(character);
        }

        [HttpPost]
        public async Task<ActionResult<List<CharacterModel>>> CreateCharacter(CharacterCreateDto request)
        {
            var newCharacter = new CharacterModel
            {
                Name = request.Name,
            };

            var backpack = new BackpackModel { Description = request.Backpack.Description, Character = newCharacter };
            var weapons = request.Weapons.Select(s => new WeaponModel() { Name = request.Name, Character = newCharacter }).ToList();
            var factions = request.Factions.Select(f => new FactionModel() { Name = request.Name, Characters = new List<CharacterModel> { newCharacter } }).ToList();

            newCharacter.Backpack = backpack;
            newCharacter.Weapons = weapons;
            newCharacter.Factions = factions;

            _datasContext.Characters.Add(newCharacter);
            await _datasContext.SaveChangesAsync();

            return Ok(await _datasContext.Characters.Include(c => c.Backpack).Include(c => c.Weapons).ToArrayAsync());
        }
    }
}
