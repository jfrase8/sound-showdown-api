using System.Reflection.Metadata;

namespace SoundShowdownGame
{

    public class Player(string id) : IBattleEntity
    {
        public const int DefaultHealth = 10; // Starting Health Points
        public const int DefaultCoins = 20; // Starting amount of coins
        public const int DefaultActions = 3; // Starting amount of actions

        public string Id { get; init; } = id; // Unique player id
        public GenreName? Genre { get; set; } // Chosen class (Genre) of the player
        public int Health { get; set; } = DefaultHealth; // Total current health points
        public int Coins { get; set; } = DefaultCoins; // Total current coins
        public List<Instrument> Instruments { get; set; } = []; // List of instruments that the player owns
        public Dictionary<Resource, int> ResourceInventory { get; set; } = []; // List of resources that is currently in the players inventory
        public IBattleEntity? Opponent { get; set; } // The opponent that the player is facing (Enemy or Musician)
        public Type EntityType { get; set; } = typeof(Player);
        public bool IsDefeated { get; set; } = false; // True if the player runs out of health

        public void TakeDamage(int damage)
        {
            Health -= damage;

            // Check if you ran out of health points
            if (Health <= 0) IsDefeated = true;
        }

        // If a player is defeated, check whether they were defeated by a musician or an enemy
        public void Defeated()
        {
            if (Opponent is Enemy enemy)
            {
                // Code for when player is defeated by enemy
                // NEEDS IMPLEMENTATION
            }
            else if (Opponent is Musician musician)
            {
                // Code for when player is defeated by musician
                // NEEDS IMPLEMENTATION
            }
        }

        // Add the resources to your resource inventory
        public void GainResources(Dictionary<Resource, int> resources)
        {
            // Combine two dictionaries
            foreach (var kvp in resources)
            {
                // If player already has at least 1 of this resource, add to that resource
                if (ResourceInventory.ContainsKey(kvp.Key))
                {
                    ResourceInventory[kvp.Key] += kvp.Value;
                }
                // Otherwise, create a new entry with that resource and the amount that player is gaining
                else
                {
                    ResourceInventory[kvp.Key] = kvp.Value;
                }
            }
        }
    }
}
