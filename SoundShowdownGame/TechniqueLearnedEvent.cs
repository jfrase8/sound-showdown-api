namespace SoundShowdownGame
{
    public class TechniqueLearnedEvent(Player player, Upgrade technique) : SoundShowdownEventArgs(SoundShowdownEventType.TechniqueLearned)
    {
        public Player Player { get; private set; } = player;
        public Upgrade Technique { get; private set; } = technique;
    }
}