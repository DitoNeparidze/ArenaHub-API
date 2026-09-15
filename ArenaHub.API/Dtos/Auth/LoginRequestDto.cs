using System.ComponentModel.DataAnnotations;

namespace ArenaHub.API.Dtos.Auth
{
    public class LoginRequestDto
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
