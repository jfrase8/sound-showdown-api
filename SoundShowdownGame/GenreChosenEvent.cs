namespace SoundShowdownGame
{
    public class GenreChosenEvent(Player player, GenreName genre) : SoundShowdownEventArgs(SoundShowdownEventType.GenreChosen)
    {
        public Player Player { get; private set; } = player;
        public GenreName Genre { get; private set; } = genre;
    }
}