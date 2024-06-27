namespace SoundShowdownGame
{
    public class GenreChosenEventArgs(SoundShowdownEventType eventType, Player player, GenreName genre) : SoundShowdownEventArgs(eventType)
    {
        public Player Player { get; private set; } = player;
        public GenreName Genre { get; private set; } = genre;
    }
}