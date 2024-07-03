namespace SoundShowdownGame
{
    public class Enemy(string name, string description, int health, int damage, Dictionary<Resource, int> loot, InstrumentType weakness, InstrumentType resistance)
    {
        public string Name { get; set; } = name; // Name of the enemy
        public string Description { get; set; } = description; // Description of the enemy
        public int DefaultHealth { get; set; } = health; // Starting health of the enemy
        public int Health { get; set; } = health; // Total current health 
        public int Damage { get; set; } = damage; // Damage that the enemy deals on hit
        public Dictionary<Resource, int> Loot { get; set; } = loot; // resources that the enemy drops
        public Player? AttackingPlayer { get; set; } // The player that is fighting this enemy
        public bool IsDefeated { get; set; } = false; // True if the enemy runs out of health
        public InstrumentType Weakness { get; set; } = weakness;
        public InstrumentType Resistance { get; set; } = resistance;
        public void TakeDamage(int damage)
        {
            Health -= damage;

            // Check if you ran out of health points
            if (Health <= 0)
            {
                IsDefeated = true;
                Defeated();
            }
        }

        // Defeated() is called when an enemy has run out of health.
        public void Defeated()
        {
            // When an enemy is defeated, drop its loot (give that loot to the player)
            if (AttackingPlayer == null) throw new SoundShowdownException("The attacking player has not been assigned to this enemy.");
            
            AttackingPlayer.Inventory += Loot;
        }
    }
}


