namespace SoundShowdownGame
{
    public class Enemy(string name, string description, int health, int damage, Dictionary<Resource, int> loot) : IBattleEntity
    {
        public string Name { get; set; } = name; // Name of the enemy
        public string Description { get; set; } = description; // Description of the enemy
        public int DefaultHealth { get; set; } = health; // Starting health of the enemy
        public int Health { get; set; } = health; // Total current health 
        public int Damage { get; set; } = damage; // Damage that the enemy deals on hit
        public Dictionary<Resource, int> Loot { get; set; } = loot; // resources that the enemy drops
        public Type EntityType { get; set; } = typeof(Enemy); // The class that this entity is
        public bool IsDefeated { get; set; } = false; // True if the enemy runs out of health
        public void TakeDamage(int damage)
        {
            Health -= damage;

            // Check if you ran out of health points
            if (Health <= 0) IsDefeated = true;
        }

        // Defeated() is called when an enemy has ran out of health.
        public void Defeated()
        {
            // When an enemy is defeated, drop its loot (give that loot to the player)
            //Player player = (Player)Opponent; // Opponent will always be player
            //player.GainResources(Loot);
        }
    }
}


