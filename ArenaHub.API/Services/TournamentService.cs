using ArenaHub.API.Dtos.Tournaments;
using ArenaHub.API.Entities;
using ArenaHub.API.Repositories;
using AutoMapper;

namespace ArenaHub.API.Services
{
    public class TournamentService : ITournamentService
    {
        private readonly ITournamentRepository _tournamentRepository;
        private readonly IMapper _mapper;
        private readonly IGameRepository _gameRepository;

        public TournamentService(ITournamentRepository tournamentRepository, IMapper mapper, IGameRepository gameRepository)
        {
            _tournamentRepository = tournamentRepository;
            _mapper = mapper;
            _gameRepository = gameRepository;
        }
        public async Task<List<TournamentDto>> GetAllAsync()
        {
            var tournaments = await _tournamentRepository.GetAllAsync();
            return _mapper.Map<List<TournamentDto>>(tournaments);
        }

        public async Task<TournamentDto?> GetByIdAsync(Guid id)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            if (tournament == null)
                return null;

            return _mapper.Map<TournamentDto>(tournament);
        }

        public async Task<TournamentDto> CreateAsync(CreateTournamentRequestDto request, string organizerId)
        {
            var existingTournament = await _tournamentRepository.ExistsByNameAsync(request.Name,Guid.Empty);
            if (existingTournament)
                throw new InvalidOperationException("Tournament with this name already exists");

            var existingGame = await _gameRepository.GetByIdAsync(request.GameId);
            if (existingGame == null)
                throw new InvalidOperationException("Game with this ID does not exist");

            var tournament = _mapper.Map<Tournament>(request);
            tournament.OrganizerId = organizerId;
            tournament.Status = TournamentStatus.Draft;
            tournament.CreatedAt = DateTime.UtcNow;

            var created = await _tournamentRepository.CreateAsync(tournament);
            return (await GetByIdAsync(created.Id))!;
        }

        public async Task<TournamentDto> OpenAsync(Guid id, string currentUserId)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            if (tournament == null)
                throw new InvalidOperationException("Tournament with this ID does not exist");
            if(tournament.OrganizerId != currentUserId)
                throw new UnauthorizedAccessException("This user does not have permission to change tournament details");
            if (tournament.Status != TournamentStatus.Draft)
                throw new InvalidOperationException("Tournament has already been opened");
            tournament.Status = TournamentStatus.Open;
            await _tournamentRepository.UpdateAsync(tournament);
            
            return _mapper.Map<TournamentDto>(tournament);
        }
        public async Task<TournamentParticipantDto> JoinAsync(Guid id, string currentUserId)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            if (tournament == null)
                throw new InvalidOperationException("Tournament not found");
            if (tournament.Status != TournamentStatus.Open)
                throw new InvalidOperationException("Tournament is not open for registration");
            if (tournament.Participants.Count >= tournament.MaxParticipants)
                throw new InvalidOperationException("Tournament is full");
            if (tournament.Participants.Any(t => t.UserId == currentUserId))
                throw new InvalidOperationException("User already joined");

            var tournamentParticipant = new TournamentParticipant
            {
                TournamentId = tournament.Id,
                UserId = currentUserId,
                JoinedAt = DateTime.UtcNow
            };
            tournament.Participants.Add(tournamentParticipant);
            await _tournamentRepository.UpdateAsync(tournament);

            return _mapper.Map<TournamentParticipantDto>(tournamentParticipant);
        }
        public async Task<TournamentDto> CancelAsync(Guid id, string currentUserId)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            if(tournament == null)
                throw new InvalidOperationException("Tournament not found");
            if(currentUserId != tournament.OrganizerId)
                throw new UnauthorizedAccessException("This user does not have permission to change tournament details");
            if (tournament.Status != TournamentStatus.Draft && tournament.Status != TournamentStatus.Open)
                throw new InvalidOperationException("Tournament cannot be cancelled");

            tournament.Status = TournamentStatus.Cancelled;
            await _tournamentRepository.UpdateAsync(tournament);

            return _mapper.Map<TournamentDto>(tournament);
        }
        public async Task<TournamentDto> StartAsync(Guid id, string currentUserId)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            if (tournament == null)
                throw new InvalidOperationException("Tournament not found");
            if (currentUserId != tournament.OrganizerId)
                throw new UnauthorizedAccessException("This user does not have permission to change tournament details");
            if (tournament.Status != TournamentStatus.Open)
                throw new InvalidOperationException("Tournament can only be started when its status is Open");

            tournament.Status = TournamentStatus.InProgress;
            await _tournamentRepository.UpdateAsync(tournament);

            return _mapper.Map<TournamentDto>(tournament);
        }
        public async Task<TournamentDto> CompleteAsync(Guid id, string currentUserId)
        {
            var tournament = await _tournamentRepository.GetByIdAsync(id);
            if (tournament == null)
                throw new InvalidOperationException("Tournament not found");
            if (tournament.OrganizerId != currentUserId)
                throw new UnauthorizedAccessException("This user doesnot have permission to change tournament details");
            if (tournament.Status != TournamentStatus.InProgress)
                throw new InvalidOperationException("Tournament cannot be completed in this state");

            tournament.Status = TournamentStatus.Completed;
            await _tournamentRepository.UpdateAsync(tournament);

            return _mapper.Map<TournamentDto>(tournament);
        }

        public async Task<TournamentDto?> UpdateAsync(Guid id, UpdateTournamentRequestDto request, string currentUserId, bool isAdmin)
        {
            var existingTournament = await _tournamentRepository.GetByIdAsync(id);
            if (existingTournament == null)
                return null;

            if (existingTournament.OrganizerId != currentUserId && !isAdmin)
                throw new UnauthorizedAccessException("This user does not have permission to change tournament details");
            
            var existingName = await _tournamentRepository.ExistsByNameAsync(request.Name,id);
            if (existingName)
                throw new InvalidOperationException("Tournament with this name already exists");


            existingTournament.Name = request.Name;
            existingTournament.MaxParticipants = request.MaxParticipants;

            var updatedTournament = await _tournamentRepository.UpdateAsync(existingTournament);

            return _mapper.Map<TournamentDto>(updatedTournament);
        }
        public async Task<TournamentDto?> DeleteAsync(Guid id, string currentUserId, bool isAdmin)
        {
            var existingTournament = await _tournamentRepository.GetByIdAsync(id);
            if (existingTournament == null)
                return null;

            if (existingTournament.OrganizerId != currentUserId && !isAdmin)
                throw new UnauthorizedAccessException("This user does not have permission to delete tournament");

            var tournament = await _tournamentRepository.DeleteAsync(existingTournament);

            return _mapper.Map<TournamentDto>(tournament);
        }

    }
}
