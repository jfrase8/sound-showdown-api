using SoundShowdownGame.Enums;

namespace SoundShowdownGame
{
    public class ActionChosenEvent(Player player, Action choseAction) : SoundShowdownEventArgs(SoundShowdownEventType.ActionChosen)
    {
        public Player Player { get; private set; } = player;
        public Action ChoseAction { get; private set; } = choseAction;
    }
}