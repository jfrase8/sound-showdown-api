using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace SoundShowdownGame
{
    public class Upgrade(UpgradeName name, UpgradeType type, string description, UpgradeEffectType[] effectType, int extraDamage)
    {
        public UpgradeName Name { get; set; } = name;
        public UpgradeType Type { get; set; } = type;
        public string Description { get; set; } = description;
        public UpgradeEffectType[] EffectType { get; set; } = effectType;
        public int ExtraDamage { get; set; } = extraDamage;
        public int RollIncrease { get; set; } = 0;
    }
}