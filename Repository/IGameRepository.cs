using TA_Apricode.Data;

namespace TA_Apricode.Repository
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetGames(string genre);
        Task<Game> GetGame(int id);
        Task AddGame(Game game);
        Task UpdateGame(Game game);
        Task DeleteGame(int id);
    }
}
