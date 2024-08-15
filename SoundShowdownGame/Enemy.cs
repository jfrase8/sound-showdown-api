using System.Numerics;

namespace SoundShowdownGame
{
    public class Enemy(string name, string description, int health, int damage, Dictionary<ResourceName, int> loot, InstrumentType weakness, InstrumentType resistance, StatusEffect statEffect)
    {
        public string Name { get; set; } = name; // Name of the enemy
        public string Description { get; set; } = description; // Description of the enemy
        public int DefaultHealth { get; set; } = health; // Starting health of the enemy
        public int Health { get; set; } = health; // Total current health 
        public int Damage { get; set; } = damage; // Damage that the enemy deals on hit
        public Dictionary<ResourceName, int> Loot { get; set; } = loot; // resources that the enemy drops
        public Player? AttackingPlayer { get; set; } // The player that is fighting this enemy
        public bool IsDefeated => Health <= 0; // True if the enemy runs out of health
        public InstrumentType Weakness { get; set; } = weakness;
        public InstrumentType Resistance { get; set; } = resistance;
        public StatusEffect? StatEffect { get; set; } = statEffect;
        public void TakeDamage(int damage)
        {
            Health -= damage;

            // Check if you ran out of health points
            if (Health <= 0)
            {
                Defeated();
            }
        }

        // Defeated() is called when an enemy has run out of health.
        public void Defeated()
        {
            if (AttackingPlayer == null) throw new SoundShowdownException("The attacking player has not been assigned to this enemy.");
            
            // Player gets loot
            AttackingPlayer.Inventory += Loot;

            // Player gets body exp equal to enemy health
            AttackingPlayer.BodyExp += DefaultHealth; // TODO : Add enemy buff counters that double enemy health
        }
    }
}


