using Microsoft.EntityFrameworkCore;
using TA_Apricode.Data;

namespace TA_Apricode.Repository
{
    public class GameRepository(GameContext context) : IGameRepository
    {
        private readonly GameContext _context = context;

        public async Task<IEnumerable<Game>> GetGames(string genre)
        {
            if (string.IsNullOrEmpty(genre))
                return await _context.Games.ToListAsync();

            return await _context.Games.Where(g => g.Genres.Contains(genre)).ToListAsync();
        }

        public async Task<Game> GetGame(int id) => await _context.Games.FindAsync(id);

        public async Task AddGame(Game game)
        {
            _context.Games.Add(game);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGame(Game game)
        {
            _context.Entry(game).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGame(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }
    }
}
