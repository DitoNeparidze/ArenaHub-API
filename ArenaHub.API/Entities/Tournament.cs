namespace ArenaHub.API.Entities
{
    public class Tournament
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public Guid GameId { get; set; }
        public string OrganizerId { get; set; } = string.Empty;
        public TournamentStatus Status { get; set; }
        public int MaxParticipants { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Game Game { get; set; } = null!;
        public ApplicationUser Organizer { get; set; } = null!;
    }
}
