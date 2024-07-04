using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace SoundShowdownGame
{
    public class Upgrade(UpgradeName name, UpgradeType type, string description, UpgradeEffectType[] effectType, int extraDamage, int rollIncrease, int healthIncrease, Dictionary<Resource, int> buildCost)
    {
        public UpgradeName Name { get; init; } = name;
        public UpgradeType Type { get; init; } = type;
        public string Description { get; init; } = description;
        public UpgradeEffectType[] EffectType { get; init; } = effectType;
        public int ExtraDamage { get; init; } = extraDamage;
        public int RollIncrease { get; init; } = rollIncrease;
        public int HealthIncrease { get; init; } = healthIncrease;
        public Dictionary<Resource, int> BuildCost { get; init; } = buildCost;
    }
}