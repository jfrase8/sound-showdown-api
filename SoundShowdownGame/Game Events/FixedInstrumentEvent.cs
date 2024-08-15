namespace SoundShowdownGame
{
    public class FixedInstrumentEvent(Player player, InstrumentType type) : SoundShowdownEventArgs(SoundShowdownEventType.FixedInstrument)
    {
        public Player Player { get; private set; } = player;
        public InstrumentType Type { get; private set; } = type;
    }
}