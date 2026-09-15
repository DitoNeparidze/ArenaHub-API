using ArenaHub.API.Dtos.Games;
using ArenaHub.API.Entities;
using ArenaHub.API.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ArenaHub.API.Controllers
{
    [Route("api/games")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GamesController(IGameRepository gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var games = await _gameRepository.GetAllAsync();
            return Ok(_mapper.Map<List<GameDto>>(games));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var game = await _gameRepository.GetByIdAsync(id);
            return game is null ? NotFound() : Ok(_mapper.Map<GameDto>(game));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGameRequestDto request)
        {
            if (request.MinPlayers > request.MaxPlayers)
                return BadRequest("MinPlayers cannot be greater than MaxPlayers.");

            var game = _mapper.Map<Game>(request);
            game = await _gameRepository.CreateAsync(game);
            return CreatedAtAction(nameof(GetById), new { id = game.Id }, _mapper.Map<GameDto>(game));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateGameRequestDto request)
        {
            if (request.MinPlayers > request.MaxPlayers)
                return BadRequest("MinPlayers cannot be greater than MaxPlayers.");

            var game = await _gameRepository.UpdateAsync(id,_mapper.Map<Game>(request));
            if (game is null)
                return NotFound();

            return Ok(_mapper.Map<GameDto>(game));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var game = await _gameRepository.DeleteAsync(id);
            if (game is null)
                return NotFound();

            return Ok(_mapper.Map<GameDto>(game));
        }

    }
}
