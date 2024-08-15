namespace SoundShowdownGame
{
    public class BackToChooseUpgradeEvent(Player player) : SoundShowdownEventArgs(SoundShowdownEventType.BackToChooseUpgrade)
    {
        public Player Player { get; private set; } = player;
    }
}