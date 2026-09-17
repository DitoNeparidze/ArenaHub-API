using System.ComponentModel.DataAnnotations;

namespace ArenaHub.API.Dtos.Tournaments
{
    public class UpdateTournamentRequestDto
    {
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public required string Name { get; set; }
        [Range(2,64)]
        public int MaxParticipants { get; set; }
    }
}
