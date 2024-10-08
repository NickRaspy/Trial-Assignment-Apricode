using Microsoft.EntityFrameworkCore;

namespace TA_Apricode.Data
{
    public class GameContext(DbContextOptions<GameContext> options) : DbContext(options)
    {
        public DbSet<Game> Games => Set<Game>();
    }
}
