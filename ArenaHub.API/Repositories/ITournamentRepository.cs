
using ArenaHub.API.Entities;

namespace ArenaHub.API.Repositories

{
    public interface ITournamentRepository
    {
        Task<List<Tournament>> GetAllAsync();
        Task<Tournament?> GetByIdAsync(Guid id);
        Task<Tournament> CreateAsync(Tournament tournament);
        Task<Tournament> UpdateAsync(Tournament tournament);
        Task<Tournament> DeleteAsync(Tournament tournament);
        Task<bool> ExistsByNameAsync(string name, Guid excludedId);
    }
}
