using System.Reflection.Metadata;

namespace SoundShowdownGame
{
    
    public class Player(string id, Genre genre) : IBattleEntity
    {
        public const int DefaultHealth = 10; // Starting Health Points
        public const int DefaultCoins = 20; // Starting amount of coins
        public const int DefaultActions = 3; // Starting amount of actions

        public string ID { get; init; } = id; // Unique player id
        public Genre Genre { get; init; } = genre; // Chosen class (Genre) of the player
        public int Health { get; set; } = DefaultHealth; // Total current health points
        public int Coins { get; set; } = DefaultCoins; // Total current coins
        public int ActionsCount { get; set; } = DefaultActions; // Current amount of actions left
        public List<Instrument> Instruments { get; set; } = []; // List of instruments that the player owns
        public Dictionary<Resource, int> ResourceInventory { get; set; } = []; // List of resources that is currently in the players inventory
        public IBattleEntity? Opponent { get; set; } // The opponent that the player is facing (Enemy or Musician)
        public Type EntityType { get; set; } = typeof(Player);
        public bool IsDefeated { get; set; } = false; // True if the player runs out of health


        public enum Action
        {
            FightEnemies,
            ChallengeMusician,
            Train,
            Shop,
            Scavenge,
            UpgradeInstruments
        }

        public void TakeAction(Action action)
        {
            switch (action)
            {
                case Action.FightEnemies:
                    FightEnemies();
                    break;
                case Action.ChallengeMusician:
                    ChallengeMusician();
                    break;
                case Action.Train:
                    Train();
                    break;
                case Action.Shop:
                    Shop();
                    break;
                case Action.Scavenge:
                    Scavenge();
                    break;
                case Action.UpgradeInstruments:
                    UpgradeInstruments();
                    break;
            }
        }

        // FightEnemies an an action the player can take that lets them battle up to 3 enemies to get loot
        public void FightEnemies()
        {
            // Use up one action
            ActionsCount -= 1;

            int enemiesDefeated = 0;

            while (enemiesDefeated < 3)
            {
                // Draw an enemy from the enemies deck (NEEDS IMPLEMENTATION)
                /* Example */
                Dictionary<Resource, int> enemyLoot = new() { { new Resource("Leather", "Old dried up leather", 4), 2 } };
                Enemy randomEnemy = new("Zombie", "An undead creature of the wasteland", 10, 10, enemyLoot, this);

                // Set this enemy to be your opponent
                Opponent = randomEnemy;

                // Start the fight, the return the winner of the fight
                Type winner = StartFight();

                // You won
                if (winner.Name == "Player")
                {
                    Opponent.Defeated();
                    enemiesDefeated++;

                    // Ask player if they want to fight another enemy (NEEDS IMPLEMENTATION)
                    /* Example */
                    bool fightAnother = false;
                    if (!fightAnother) break;
                }
                // Opponent won
                else
                {
                    Defeated();
                    break;
                }
            }
        }

        // Returns the winner of a fight between Player and Opponent
        public Type StartFight()
        {
            Type result;
            while (true) {
                // You attack first
                Attack();

                // Check if opponent is defeated
                if (Opponent.IsDefeated)
                {
                    result = EntityType;
                    break;
                }

                // Opponent attacks
                Opponent.Attack();

                // Check if you are defeated
                if (IsDefeated)
                {
                    result = Opponent.EntityType;
                    break;
                }
            } 
            return result;
        }

        public void Attack()
        {
            // Roll a random number on 6-sided die
            int roll = Dice.RollDie();

            // Add code to figure out the damage of the weapon
            int damage = CalcDamage(roll);

            Opponent.TakeDamage(damage);// IBattleEntity implements TakeDamage() function
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
        public int CalcDamage(int roll)
        {
            return 0; // NEEDS IMPLEMENTATION
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
