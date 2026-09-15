using System.ComponentModel.DataAnnotations;

namespace ArenaHub.API.Dtos.Auth
{
    public class RegisterRequestDto
    {
        [Required]
        [StringLength(25, MinimumLength = 3)]
        public required string UserName { get; set; }
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        [StringLength(80, MinimumLength = 6)]
        public required string Password { get; set; }
    }
}
