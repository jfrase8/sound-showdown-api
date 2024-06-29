using System.Reflection.Metadata;
using System.Resources;

namespace SoundShowdownGame
{

    public class Player(string id)
    {
        public const int DefaultHealth = 10; // Starting Health Points
        public const int DefaultActions = 3; // Starting amount of actions

        public string Id { get; init; } = id; // Unique player id
        public GenreName? Genre { get; set; } // Chosen class (Genre) of the player
        public int Health { get; set; } = DefaultHealth; // Total current health points
        public List<Instrument> Instruments { get; set; } = []; // List of instruments that the player owns
        public Inventory Inventory { get; set; } = new();
        public Enemy? Enemy { get; set; } // The opponent that the player is facing
        public Type EntityType { get; set; } = typeof(Player);
        public bool IsDefeated { get; set; } = false; // True if the player runs out of health

        public void TakeDamage<T>(int damage, T opponent)
        {
            Health -= damage;

            // Check if you ran out of health points
            if (Health <= 0)
            {
                IsDefeated = true;
                Defeated(opponent);
            }
        }

        // If a player is defeated, check whether they were defeated by a musician or an enemy
        public void Defeated<T>(T opponent)
        {
            switch (opponent)
            {
                case Enemy enemy:
                    // NEEDS IMPLEMENTATION
                    break;
                case Musician musician:
                    // NEEDS IMPLEMENTATION
                    break;
                case null:
                    throw new SoundShowdownException("Opponent is null. Opponent must be an Enemy or Musician.");
                default:
                    throw new SoundShowdownException($"Opponent is of type {opponent.GetType()}. Must be of type Enemy or Musician");
            }
        }
    }
}
