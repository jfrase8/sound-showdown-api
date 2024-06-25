namespace SoundShowdownGame
{
    public class GenreChosenEvent
    {
        public string PlayerID { get; set; }
        public GenreName Genre { get; set; }

        public GenreChosenEvent(string playerID, GenreName genre)
        {
            PlayerID = playerID;
            Genre = genre;
        }
    }
}