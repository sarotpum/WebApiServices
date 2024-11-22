using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharedService.DBContext;
using SharedService.Models.Card;

namespace WebApiServices.Controllers.CardsController
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly DatasContext _datasContext;

        public CardsController(DatasContext datasContext)
        {
            _datasContext = datasContext;
        }

        // Get All Cards
        [HttpGet()]
        public async Task<IActionResult> GetAllCards()
        {
            var cards = await _datasContext.Cards.ToListAsync();
            return Ok(cards);
        }

        // Get single card
        [HttpGet()]
        [Route("{id:guid}")]
        [ActionName("GetCard")]
        public async Task<IActionResult> GetCard([FromRoute] Guid id)
        {
            var cards = await _datasContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (cards != null)
            {
                return Ok(cards);
            }
            return NotFound("Card not found");
        }

        // Add card
        [HttpPost]
        public async Task<IActionResult> AddAllCards([FromBody] CardModel card)
        {
            card.Id = Guid.NewGuid();

            await _datasContext.Cards.AddAsync(card);
            await _datasContext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetCard), new { id = card.Id }, card);
        }

        // Update A Card
        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateCard([FromRoute] Guid id, [FromBody] CardModel card)
        {
            var existingCard = await _datasContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existingCard != null)
            {
                existingCard.CardholderName = card.CardholderName;
                existingCard.CardNumber = card.CardNumber;
                existingCard.ExpiryMonth = card.ExpiryMonth;
                existingCard.ExpiryYear = card.ExpiryYear;
                existingCard.CVC = card.CVC;
                await _datasContext.SaveChangesAsync();
                return Ok(existingCard);
            }

            return NotFound("Card not found");
        }

        // Delete A Card
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteCard([FromRoute] Guid id)
        {
            var existCard = await _datasContext.Cards.FirstOrDefaultAsync(x => x.Id == id);
            if (existCard != null)
            {
                _datasContext.Remove(existCard);
                await _datasContext.SaveChangesAsync();
                return Ok(existCard);
            }
            return Ok("Card not found");
        }
    }
}
