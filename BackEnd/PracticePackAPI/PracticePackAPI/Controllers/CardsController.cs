using Microsoft.AspNetCore.Mvc;
using PracticePackAPI.Models;
using PracticePackAPI.Services;

namespace PracticePackAPI.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CardsController : ControllerBase
    {
        private readonly iService<Card> _cardService;

        public CardsController(iService<Card> CardService)
        {
            _cardService = CardService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Card>> GetCards()
        {
            return _cardService.GetAllWithData().ToArray();
        }

        [HttpGet("filter/{identity}")]
        public ActionResult<IEnumerable<Card>> GetFilterCards(Guid identity)
        {
            return _cardService.GetAllWithData(c => c.UserId == identity).ToArray();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCard(Guid id)
        {
            return await _cardService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult<Card>> CreateCard(Card card)
        {
            return await _cardService.Create(card);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Card>> UpdateCard(Guid id, Card card)
        {
            if(id != card.Id)
                return BadRequest();

            var updatedEntity = await _cardService.Update(card);

            if(updatedEntity == null)
                return NotFound();

            return updatedEntity;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCard(Guid id)
        {
            if(await _cardService.GetById(id) == null)
                return NotFound();

            await _cardService.Delete(id);

            return NoContent();
        }
    }
}