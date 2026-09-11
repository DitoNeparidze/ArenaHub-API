namespace ArenaHub.API.Dtos.Games
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Genre { get; set; }
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
    }
}
