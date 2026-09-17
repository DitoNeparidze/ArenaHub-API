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
            return await _dbContext.Tournaments.ToListAsync();
        }

        public async Task<Tournament?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Tournaments.FindAsync(id);
        }
        public async Task<Tournament> CreateAsync(Tournament tournament)
        {
            _dbContext.Tournaments.Add(tournament);
            await _dbContext.SaveChangesAsync();

            return tournament;
        }

        public async Task<Tournament?> UpdateAsync(Guid id, Tournament tournament)
        {
            var existingTournament = await _dbContext.Tournaments.FindAsync(id);
            if (existingTournament == null)
                return null;

            existingTournament.Name = tournament.Name;
            existingTournament.MaxParticipants = tournament.MaxParticipants;

            await _dbContext.SaveChangesAsync();

            return existingTournament;
        }
        public async Task<Tournament?> DeleteAsync(Guid id)
        {
            var tournament = await _dbContext.Tournaments.FindAsync(id);
            if (tournament == null)
                return null;
            _dbContext.Tournaments.Remove(tournament);
            await _dbContext.SaveChangesAsync();

            return tournament;
        }
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await _dbContext.Tournaments.AnyAsync(t => t.Name.ToLower() == name.ToLower());
        }
    }
}
