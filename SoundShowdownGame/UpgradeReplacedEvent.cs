namespace SoundShowdownGame
{
    public class UpgradeReplacedEvent(Player player, Upgrade newUpgrade, Upgrade replacedUpgrade) : SoundShowdownEventArgs(SoundShowdownEventType.UpgradeReplaced)
    {
        public Player Player { get; private set; } = player;
        public Upgrade NewUpgrade { get; private set; } = newUpgrade;
        public Upgrade ReplacedUpgrade { get; private set; } = replacedUpgrade;
    }
}