namespace SoundShowdownGame
{
    public class UpgradeBuiltEvent(Player player, Upgrade upgrade) : SoundShowdownEventArgs(SoundShowdownEventType.UpgradeBuilt)
    {
        public Player Player { get; private set; } = player;
        public Upgrade Upgrade { get; private set; } = upgrade;
    }
}