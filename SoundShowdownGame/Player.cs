namespace SoundShowdownGame
{
    // IBattleEntity implements TakeDamage() function
    public class Player(string id, Genre genre) : IBattleEntity
    {
        public const int DefaultHealth = 10; // Starting Health Points
        public const int DefaultCoins = 20; // Starting amount of coins

        public string ID { get; init; } = id; // Unique player id
        public Genre Genre { get; init; } = genre; // Chosen class (Genre) of the player
        public int Health { get; set; } = DefaultHealth; // Total current health points
        public int Coins { get; set; } = DefaultCoins; // Total current coins
        public List<Instrument> Instruments { get; set; } = []; // List of instruments that the player owns
        public List<Loot> LootInventory { get; set; } = []; // List of loot that is currently in the players inventory
        public IBattleEntity? Opponent { get; set; } // The opponent that the player is facing (Enemy or Musician)

        public void Attack()
        {
            // Add code to figure out the damage of the weapon
            int damage = CalcDamage();

            Opponent.TakeDamage(damage);
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

        // CalcDamage calculates the total damage that the players instrument does to an enemy/musician
        public int CalcDamage()
        {
            return 0; // NEEDS IMPLEMENTATION
        }
    }
}
