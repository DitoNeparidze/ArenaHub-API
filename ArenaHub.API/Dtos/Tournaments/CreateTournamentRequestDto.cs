using System.ComponentModel.DataAnnotations;

namespace ArenaHub.API.Dtos.Tournaments
{
    public class CreateTournamentRequestDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public required string Name { get; set; }
        public Guid GameId { get; set; }
        [Range(2,64)]
        public int MaxParticipants { get; set; }
    }
}
