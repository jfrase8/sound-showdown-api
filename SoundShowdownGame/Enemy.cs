namespace SoundShowdownGame
{
    // IBattleEntity implements TakeDamage() function
    public class Enemy(string name, string description, int health, int damage, int loot, Player player) : IBattleEntity
    {
        public string Name { get; set; } = name; // Name of the enemy
        public string Description { get; set; } = description; // Description of the enemy
        public int DefaultHealth { get; set; } = health; // Starting health of the enemy
        public int Health { get; set; } = health; // Total current health 
        public int Damage { get; set; } = damage; // Damage that the enemy deals on hit
        public int Loot { get; set; } = loot; // Loot that the enemy drops
        public IBattleEntity Opponent { get; set; } = player; // An enemy's opponent is the player

        public void Attack()
        {
            // Deal damage to the player
            Opponent.TakeDamage(Damage);
        }

        // Defeated() is called when an enemy has ran out of health.
        public void Defeated()
        {
            // When an enemy is defeated, drop its loot (give that loot to the player)
            // NEEDS IMPLEMENTATION
        }
    }
}


