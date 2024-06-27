namespace SoundShowdownGame
{
    public class ActionChosenEventArgs(Player player, Action choseAction) : EventArgs
    {
        public Player Player { get; set; } = player;
        public Action ChoseAction { get; set; } = choseAction;
    }
}