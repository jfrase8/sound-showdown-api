namespace SoundShowdownGame
{
    public class UsedItemEvent(Player player, Item item) : SoundShowdownEventArgs(SoundShowdownEventType.UsedItem)
    {
        public Player Player { get; private set; } = player;
        public Item Ttem { get; private set; } = item;
    }
}