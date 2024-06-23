namespace SoundShowdownGame
{
    public class Instrument(string name, Instrument.InstrumentType type, string description, int damage, int cost, int sellValue)
    {
        public enum InstrumentType
        {
            Brass,
            Percussion,
            Vocal,
            Woodwind,
            String
        }
        public enum InstrumentName // Change later to be a database with instrument names
        {
            Piano,
            Flute,
            Mic,
            Drums,
            Sampler,
            TalkBox,
            Trumpet,
            Trombone,
            Clarinet,
            Violin,
            ElectricGuitar,
            Autotune,
            BassGuitar
        }

        public string Name { get; set; } = name;
        public InstrumentType Type { get; set; } = type; // Type of instrument
        public string Description { get; set; } = description;
        public int Damage { get; set; } = damage;
        public int Cost { get; set; } = cost; // Amount of coins needed to buy this instruments
        public int SellValue { get; set; } = sellValue; // How many coins this instrument sells for


    }
}
