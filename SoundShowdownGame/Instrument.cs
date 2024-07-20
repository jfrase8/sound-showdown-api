using System.Runtime.Serialization.Formatters;

namespace SoundShowdownGame
{
    public partial class Instrument(InstrumentName name, InstrumentType type, string description, int damage, int cost, int sellValue, Quality quality, List<GenreName> genreBonuses, List<Upgrade> upgrades)
    {
        public InstrumentName Name { get; set; } = name;
        public InstrumentType Type { get; set; } = type; // Type of instrument
        public string Description { get; set; } = description;
        public int Damage { get; set; } = damage; // Base damage that an instrument starts with
        public int Cost { get; set; } = cost; // Amount of coins needed to buy this instruments
        public Quality Quality { get; set; } = quality;
        public int SellValue { get; set; } = sellValue; // How many coins this instrument sells for
        public List<GenreName> GenreBonuses {  get; set; } = genreBonuses; // Genres that will benefit when using this instrument
        public List<Upgrade> Upgrades { get; set; } = upgrades;
        public int Level { get; set; } = 1;
        public List<StatusEffect> StatusEffects { get; set; } = [];
        public List<Upgrade> Techniques { get; set; } = [];
        public int DamageCounters { get; set; } = 0;

        public int GetDamageFromUpgrades()
        {
            int extraDamage = 0;
            foreach (var upgrade in Upgrades)
            {
                extraDamage += upgrade.ExtraDamage;
            }
            return extraDamage;
        }
        public int GetRollIncreaseFromUpgrades()
        {
            int rollIncrease = 0;
            foreach (var upgrade in Upgrades)
            {
                rollIncrease += upgrade.RollIncrease;
            }
            return rollIncrease;
        }
    }
}
