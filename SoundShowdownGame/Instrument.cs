using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters;

namespace SoundShowdownGame
{
    public class Instrument
    {
        public InstrumentName Name { get; set; }
        public InstrumentType Type { get; set; } // Type of instrument
        public string Description { get; set; }
        public int Damage { get; set; } // Base damage that an instrument starts with
        public int Cost { get; set; } // Amount of coins needed to buy this instruments
        public Quality Quality { get; set; }
        public int SellValue { get; set; } // How many coins this instrument sells for
        public List<GenreName> GenreBonuses {  get; set; } // Genres that will benefit when using this instrument
        public List<Upgrade> Upgrades { get; set; }
        public int Level { get; set; } = 1;
        public List<StatusEffect> StatusEffects { get; set; } = [];
        public List<Upgrade> Techniques { get; set; } = [];
        public int DamageCounters { get; set; }
        public int Experience { get; set; }

        public Instrument(InstrumentName name, InstrumentType type, string description, int damage, int cost, int sellValue, Quality quality, List<GenreName> genreBonuses, List<Upgrade> upgrades)
        {
            Name = name;
            Type = type;
            Description = description;
            Damage = damage;
            Cost = cost;
            SellValue = sellValue;
            Quality = quality;
            GenreBonuses = genreBonuses;
            Upgrades = upgrades;
        }

        public Instrument(InstrumentName name, InstrumentType type, string description, int damage, int cost, int sellValue, Quality quality, List<GenreName> genreBonuses, List<Upgrade> upgrades, int level, List<StatusEffect> statusEffects, List<Upgrade> techniques, int damageCounters, int experience)
        {
            Name = name;
            Type = type;
            Description = description;
            Damage = damage;
            Cost = cost;
            SellValue = sellValue;
            Quality = quality;
            GenreBonuses = genreBonuses;
            Upgrades = upgrades;
            Level = level;
            StatusEffects = statusEffects;
            Techniques = techniques;
            DamageCounters = damageCounters;
            Experience = experience;
        }

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

        // Creates a resale version of an instrument
        public Instrument CreateResale()
        {
            return new Instrument(Name, Type, Description, Damage, Cost-10, SellValue, Quality, GenreBonuses, []);
        }
    }
}
