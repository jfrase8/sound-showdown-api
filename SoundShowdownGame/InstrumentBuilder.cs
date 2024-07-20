using SoundShowdownGame;
using static System.Net.Mime.MediaTypeNames;

namespace SoundShowdownGame
{
    public class InstrumentBuilder
    {
        private InstrumentName Name;
        private InstrumentType Type;
        public string Description;
        private List<Upgrade> Upgrades;
        private int Damage;
        private int Cost;
        private Quality Quality;
        private int SellValue;
        private List<GenreName> GenreBonuses;
        private int Level;
        private List<StatusEffect> StatusEffects;
        private List<Upgrade> Techniques;
        private int DamageCounters;

        public InstrumentBuilder()
        {
            // Defaults
            Name = InstrumentName.Guitar;
            Type = InstrumentType.String;
            Description = "";
            Upgrades = [];
            Damage = 0;
            Cost = 20;
            Quality = Quality.Low;
            SellValue = 10;
            GenreBonuses = [GenreName.Rock, GenreName.Pop, GenreName.Jazz];
            Level = 1;
            StatusEffects = [];
            Techniques = [];
            DamageCounters = 0;
        }

        public Instrument Build()
        {
            Instrument instrument = new Instrument(name: Name, type: Type, description: Description, damage: Damage, cost: Cost, sellValue: SellValue, quality: Quality, genreBonuses: GenreBonuses, upgrades: Upgrades);
            return instrument;
        }

        public InstrumentBuilder WithName(InstrumentName name)
        {
            Name = name;
            return this;
        }

        public InstrumentBuilder WithType(InstrumentType type)
        {
            Type = type;
            return this;
        }

        public InstrumentBuilder WithDamage(int damage)
        {
            Damage = damage;
            return this;
        }

        public InstrumentBuilder WithCost(int cost)
        {
            Cost = cost;
            return this;
        }

        public InstrumentBuilder WithQuality(Quality quality)
        {
            Quality = quality;
            return this;
        }

        public InstrumentBuilder WithSellValue(int sellValue)
        {
            SellValue = sellValue;
            return this;
        }

        public InstrumentBuilder WithGenreBonuses(List<GenreName> genreBonuses)
        {
            GenreBonuses = genreBonuses;
            return this;
        }

        public InstrumentBuilder WithLevel(int level)
        {
            Level = level;
            return this;
        }

        public InstrumentBuilder WithStatusEffects(List<StatusEffect> statusEffects)
        {
            StatusEffects = statusEffects;
            return this;
        }
        public InstrumentBuilder WithUpgrades(List<Upgrade> upgrades)
        {
            Upgrades = upgrades;
            return this;
        }

        public InstrumentBuilder WithTechniques(List<Upgrade> techniques)
        {
            Techniques = techniques;
            return this;
        }

        public InstrumentBuilder WithDamageCounters(int damageCounters)
        {
            DamageCounters = damageCounters;
            return this;
        }

    }
}