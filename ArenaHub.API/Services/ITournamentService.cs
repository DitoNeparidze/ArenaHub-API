using ArenaHub.API.Dtos.Tournaments;

namespace ArenaHub.API.Services
{
    public interface ITournamentService
    {
        Task<List<TournamentDto>> GetAllAsync();
        Task<TournamentDto?> GetByIdAsync(Guid id);
        Task<TournamentDto> CreateAsync(CreateTournamentRequestDto request, string organizerId);
        Task<TournamentDto> OpenAsync(Guid id, string currentUserId);
        Task<TournamentParticipantDto> JoinAsync(Guid id, string currentUserId);
        Task<TournamentDto> CancelAsync (Guid id, string currentUserId);
        Task<TournamentDto> StartAsync(Guid id, string currentUserId);
        Task<TournamentDto> CompleteAsync(Guid id, string currentUserId);
        Task<TournamentDto?> UpdateAsync(Guid id, UpdateTournamentRequestDto request, string currentUserId,bool isAdmin);
        Task<TournamentDto?> DeleteAsync(Guid id, string currentUserId, bool isAdmin);
    }
}
