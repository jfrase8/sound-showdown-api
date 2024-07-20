namespace SoundShowdownGame
{
    public class ChooseUpgradeToReplaceEvent(Player player, Upgrade upgrade) : SoundShowdownEventArgs(SoundShowdownEventType.ChooseUpgradeToReplace)
    {
        public Player Player { get; private set; } = player;
        public Upgrade Upgrade { get; private set; } = upgrade;
    }
}