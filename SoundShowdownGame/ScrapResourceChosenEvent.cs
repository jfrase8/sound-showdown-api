namespace SoundShowdownGame
{
    public class ScrapResourceChosenEvent(Player player, Resource resource) : SoundShowdownEventArgs(SoundShowdownEventType.ScrapResourceChosen)
    {
        public Player Player { get; private set; } = player;
        public Resource Resource { get; private set; } = resource;
    }
}