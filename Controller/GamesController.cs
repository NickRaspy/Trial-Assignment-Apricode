using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TA_Apricode.Data;
using TA_Apricode.Repository;

namespace TA_Apricode.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController(IGameRepository repository) : ControllerBase
    {
        private readonly IGameRepository _repository = repository;

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames(string genre)
        {
            var games = await _repository.GetGames(genre);
            return Ok(games);
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<Game>>> GetAllGames()
        {
            var games = await _repository.GetGames(string.Empty);
            return Ok(games);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            var game = await _repository.GetGame(id);

            if (game == null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            await _repository.AddGame(game);
            return CreatedAtAction(nameof(GetGame), new { id = game.Id }, game);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGame(int id, Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }

            try
            {
                await _repository.UpdateGame(game);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await GameExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var game = await _repository.GetGame(id);
            if (game == null)
            {
                return NotFound();
            }

            await _repository.DeleteGame(id);
            return NoContent();
        }

        private async Task<bool> GameExists(int id)
        {
            var game = await _repository.GetGame(id);
            return game != null;
        }
    }
}