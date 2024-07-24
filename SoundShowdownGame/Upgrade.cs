using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace SoundShowdownGame
{
    public class Upgrade
    {
        public UpgradeName Name { get; init; }
        public UpgradeType Type { get; init; }
        public string Description { get; init; }
        public int ExtraDamage { get; init; }
        public int RollIncrease { get; init; }
        public int HealthIncrease { get; init; }
        public Dictionary<ResourceName, int>? BuildCost { get; init; }

        public InstrumentType? InstrumentType { get; init; }

        public Upgrade(UpgradeName name, UpgradeType type, string description, int extraDamage, int rollIncrease, int healthIncrease, Dictionary<ResourceName, int>? buildCost)
        {
            Name = name;
            Type = type;
            Description = description;
            ExtraDamage = extraDamage;
            RollIncrease = rollIncrease;
            HealthIncrease = healthIncrease;
            BuildCost = buildCost;
        }
        public Upgrade(UpgradeName name, UpgradeType type, string description, int extraDamage, int rollIncrease, int healthIncrease, Dictionary<ResourceName, int> buildCost, InstrumentType instrumentType)
        {
            Name = name;
            Type = type;
            Description = description;
            ExtraDamage = extraDamage;
            RollIncrease = rollIncrease;
            HealthIncrease = healthIncrease;
            BuildCost = buildCost;
            InstrumentType = instrumentType;
        }
    }
}