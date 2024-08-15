
namespace SoundShowdownGame
{
    public class RolledScavengeDiceEvent(Player player, List<int> rolls, EventCard? eventCard) : SoundShowdownEventArgs(SoundShowdownEventType.RolledScavengeDice)
    {
        public Player Player { get; private set; } = player;
        public List<int> Rolls { get; private set; } = rolls;
        public EventCard? EventCard { get; private set; } = eventCard;
    }
}