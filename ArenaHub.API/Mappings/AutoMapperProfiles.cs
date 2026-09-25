using ArenaHub.API.Dtos.Games;
using ArenaHub.API.Dtos.Tournaments;
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

            CreateMap<Tournament, TournamentDto>()
                .ForMember(dest => dest.GameName,
                    opt => opt.MapFrom(src => src.Game.Name))
                .ForMember(dest => dest.OrganizerName,
                    opt => opt.MapFrom(src => src.Organizer.UserName));
            CreateMap<CreateTournamentRequestDto, Tournament>();
            CreateMap<TournamentParticipant, TournamentParticipantDto>();

        }
    }
}
