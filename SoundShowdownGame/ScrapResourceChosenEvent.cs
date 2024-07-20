namespace SoundShowdownGame
{
    public class ScrapResourceChosenEvent(Player player, ResourceName resource) : SoundShowdownEventArgs(SoundShowdownEventType.ScrapResourceChosen)
    {
        public Player Player { get; private set; } = player;
        public ResourceName Resource { get; private set; } = resource;
    }
}