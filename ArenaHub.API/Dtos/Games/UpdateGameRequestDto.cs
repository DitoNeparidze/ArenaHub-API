using System.ComponentModel.DataAnnotations;

namespace ArenaHub.API.Dtos.Games
{
    public class UpdateGameRequestDto
    {
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        [Required]
        [StringLength(50)]
        public required string Genre { get; set; }
        [Range(1, 100)]
        public int MinPlayers { get; set; }
        [Range(1, 100)]
        public int MaxPlayers { get; set; }

    }
}
