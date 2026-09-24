using ArenaHub.API.Dtos.Tournaments;

namespace ArenaHub.API.Services
{
    public interface ITournamentService
    {
        Task<List<TournamentDto>> GetAllAsync();
        Task<TournamentDto?> GetByIdAsync(Guid id);
        Task<TournamentDto> CreateAsync(CreateTournamentRequestDto request, string organizerId);
        Task<TournamentDto> OpenAsync(Guid Id, string currentUserId);
        Task<TournamentDto?> UpdateAsync(Guid id, UpdateTournamentRequestDto request, string currentUserId,bool isAdmin);
        Task<TournamentDto?> DeleteAsync(Guid id, string currentUserId, bool isAdmin);
    }
}
