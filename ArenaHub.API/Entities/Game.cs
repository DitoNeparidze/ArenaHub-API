namespace ArenaHub.API.Entities
{
    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public int MinPlayers { get; set; }
        public int MaxPlayers { get; set; }
    }
}
