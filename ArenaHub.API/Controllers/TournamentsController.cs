using ArenaHub.API.Constants;
using ArenaHub.API.Dtos.Tournaments;
using ArenaHub.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ArenaHub.API.Controllers
{
    [Route("api/tournaments")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournamentService _tournamentService;

        public TournamentsController(ITournamentService tournamentService)
        {
            _tournamentService = tournamentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tournaments = await _tournamentService.GetAllAsync();
            return Ok(tournaments);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var tournament = await _tournamentService.GetByIdAsync(id);
            if (tournament == null)
                return NotFound("Tournament with this ID does not exist");

            return Ok(tournament);
        }

        [HttpPost]
        [Authorize(Roles = AppRoles.User)]
        public async Task<IActionResult> Create(CreateTournamentRequestDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var tournament = await _tournamentService.CreateAsync(request, userId);

            return CreatedAtAction(nameof(GetById), new { id = tournament.Id }, tournament);
        }

        [HttpPost("{id:guid}/open")]
        [Authorize(Roles = AppRoles.User)]
        public async Task<IActionResult> Open(Guid id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var tournament = await _tournamentService.OpenAsync(id, currentUserId);

            return Ok(tournament);
        }

        [HttpPost("{id:guid}/join")]
        [Authorize]
        public async Task<IActionResult> Join(Guid id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var tournament = await _tournamentService.JoinAsync(id, currentUserId);

            return Ok(tournament);
        }
        [HttpPost("{id:guid}/cancel")]
        [Authorize(Roles = AppRoles.User)]
        public async Task<IActionResult> Cancel(Guid id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

            var tournament = await _tournamentService.CancelAsync(id, currentUserId);

            return Ok(tournament); 
        }

        [HttpPost("{id:guid}/start")]
        [Authorize(Roles = AppRoles.User)]
        public async Task<IActionResult> Start(Guid id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var tournament = await _tournamentService.StartAsync(id, currentUserId);

            return Ok(tournament);
        }
        [HttpPost("{id:guid}/complete")]
        [Authorize(Roles = AppRoles.User)]
        public async Task<IActionResult> Complete(Guid id)
        {
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var tournament = await _tournamentService.CompleteAsync(id, currentUserId);

            return Ok(tournament);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Roles = $"{AppRoles.User},{AppRoles.Admin}")]
        public async Task<IActionResult> Update(Guid id,UpdateTournamentRequestDto request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var isAdmin = User.IsInRole(AppRoles.Admin);
                
            var tournament = await _tournamentService.UpdateAsync(id, request, userId,isAdmin);
            if (tournament == null)
                return NotFound("Tournament with this ID does not exist");

            return Ok(tournament);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Roles = $"{AppRoles.User},{AppRoles.Admin}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            var isAdmin = User.IsInRole(AppRoles.Admin);
            var tournament = await _tournamentService.DeleteAsync(id, userId,isAdmin);
            if (tournament == null)
                return NotFound("Tournament with this ID does not exist");

            return Ok(tournament);
        }
    }
}
