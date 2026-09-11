using ArenaHub.API.Dtos.Games;
using ArenaHub.API.Entities;
using AutoMapper;

namespace ArenaHub.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Game, GameDto>();
            CreateMap<CreateGameRequestDto, Game>();
            CreateMap<UpdateGameRequestDto, Game>();
        }
    }
}
