namespace ArenaHub.API.Dtos.Tournaments
{
    public class TournamentParticipantDto
    {
        public Guid Id { get; set; }
        public Guid TournamentId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime JoinedAt { get; set; }
    }
}
