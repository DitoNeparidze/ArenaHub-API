namespace ArenaHub.API.Entities
{
    public class TournamentParticipant
    {
        public Guid Id { get; set; }
        public Guid TournamentId { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime JoinedAt { get; set; }


        // Navigation
        public Tournament Tournament { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
    }
}
