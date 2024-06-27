namespace SoundShowdownGame
{
    public class ActionChosenEventArgs(SoundShowdownEventType eventType, Player player, Action choseAction) : SoundShowdownEventArgs(eventType)
    {
        public Player Player { get; private set; } = player;
        public Action ChoseAction { get; private set; } = choseAction;
    }
}