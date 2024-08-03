using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace SoundShowdownGame
{
    public delegate void UpgradeEffect(Player player, int roll);
    public class Upgrade(UpgradeName name, UpgradeType type, string description, Dictionary<ResourceName, int>? buildCost, InstrumentType? instrumentType, UpgradeEffectType upgradeEffectType)
    {
        public UpgradeName Name { get; init; } = name;
        public UpgradeType Type { get; init; } = type;
        public string Description { get; init; } = description;
        public Dictionary<ResourceName, int>? BuildCost { get; init; } = buildCost;
        public InstrumentType? InstrumentType { get; init; } = instrumentType;
        public UpgradeEffectType UpgradeEffectType { get; init; } = upgradeEffectType;
        public UpgradeEffect? Effect { get; set; } // TODO : Add upgrade effect testing and implementation
    }
}