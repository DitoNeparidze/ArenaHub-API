using ArenaHub.API.Data;
using ArenaHub.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace ArenaHub.API.Repositories
{
    public class TournamentRepository : ITournamentRepository
    {
        private readonly ArenaHubDbContext _dbContext;

        public TournamentRepository(ArenaHubDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Tournament>> GetAllAsync()
        {
            return await _dbContext.Tournaments
                .Include(t=> t.Game)
                .Include(t=> t.Organizer)
                .ToListAsync();
        }

        public async Task<Tournament?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Tournaments
                .Include(t => t.Game)
                .Include(t => t.Organizer)
                .FirstOrDefaultAsync(t=> t.Id == id);
        }
        public async Task<Tournament> CreateAsync(Tournament tournament)
        {
            _dbContext.Tournaments.Add(tournament);
            await _dbContext.SaveChangesAsync();

            return tournament;
        }

        public async Task<Tournament> UpdateAsync(Tournament tournament)
        {
            await _dbContext.SaveChangesAsync();

            return tournament;
        }
        public async Task<Tournament> DeleteAsync(Tournament tournament)
        {
            _dbContext.Tournaments.Remove(tournament);
            await _dbContext.SaveChangesAsync();

            return tournament;
        }
        public async Task<bool> ExistsByNameAsync(string name, Guid excludeId)
        {
            return await _dbContext.Tournaments
                .AnyAsync(t => t.Id != excludeId && t.Name.ToLower() == name.ToLower());
        }
    }
}
