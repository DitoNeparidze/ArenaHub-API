using ArenaHub.API.Entities;

namespace ArenaHub.API.Repositories
{
    public interface IGameRepository
    {
        Task<List<Game>> GetAllAsync();
        Task<Game?> GetByIdAsync(Guid id);
        Task<Game> CreateAsync(Game game);
        Task<Game?> UpdateAsync(Guid id, Game game);
        Task<Game?> DeleteAsync(Guid id);
        Task<bool> ExistsByNameAsync(string name);
    }
}
