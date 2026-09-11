using ArenaHub.API.Data;
using ArenaHub.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArenaHub.API.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ArenaHubDbContext _dbContext;
        public GameRepository(ArenaHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Game>> GetAllAsync()
        {
            return await _dbContext.Games.ToListAsync();
        }

        public async Task<Game?> GetByIdAsync(Guid id)
        {
            var game = await _dbContext.Games.FindAsync(id);
            return game;
        }
        public async Task<Game> CreateAsync(Game game)
        {
            _dbContext.Games.Add(game);
            await _dbContext.SaveChangesAsync();
            return game;
        }

        public async Task<Game?> UpdateAsync(Guid id, Game game)
        {
            var existingGame = await _dbContext.Games.FindAsync(id);
            if (existingGame == null)
                return null;
            
            existingGame.Name = game.Name;
            existingGame.Genre = game.Genre;
            existingGame.MinPlayers = game.MinPlayers;
            existingGame.MaxPlayers = game.MaxPlayers;

            await _dbContext.SaveChangesAsync();

            return existingGame;
        }
        public async Task<Game?> DeleteAsync(Guid id)
        {
            var game = await _dbContext.Games.FindAsync(id);
            if (game == null)
                return null;
            _dbContext.Games.Remove(game);

            await _dbContext.SaveChangesAsync();

            return game;
        }
    }
}
