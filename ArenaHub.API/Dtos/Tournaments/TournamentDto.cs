using ArenaHub.API.Entities;

namespace ArenaHub.API.Dtos.Tournaments
{
    public class TournamentDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid GameId { get; set; }
        public string OrganizerId { get; set; } = string.Empty;
        public int MaxParticipants { get; set; }
        public TournamentStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string GameName { get; set; } = string.Empty;
        public string OrganizerName { get; set; } = string.Empty;
    }
}
