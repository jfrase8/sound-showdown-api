namespace SoundShowdownGame
{
    public class GenreChosenEventArgs(Player player, GenreName genre) : EventArgs
    {
        public Player Player { get; set; } = player;
        public GenreName Genre { get; set; } = genre;
    }
}