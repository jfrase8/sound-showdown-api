using SoundShowdownGame.Enums;

namespace SoundShowdownGame
{
    public class ActionChosenEvent(Player player, GameAction choseAction) : SoundShowdownEventArgs(SoundShowdownEventType.ActionChosen)
    {
        public Player Player { get; private set; } = player;
        public GameAction ChoseAction { get; private set; } = choseAction;
    }
}