namespace SoundShowdownGame
{
    public class UpgradeBuiltEvent(Player player, Upgrade upgrade) : SoundShowdownEventArgs(SoundShowdownEventType.UpgradeBuilt)
    {
        private Player Player = player;
        private Upgrade Upgrade = upgrade;
    }
}