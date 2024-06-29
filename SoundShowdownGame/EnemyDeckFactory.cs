using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class EnemyDeckFactory
    {
        public static Deck<Enemy> CreateShuffledDeck()
        {
            // Default loot
            Dictionary<Resource, int> loot = new Dictionary<Resource, int>
            {
                { new Resource("Leather", "Dry leather", 5), 2 }
            };

            // Create deck
            Enemy[] cards =
            [
                new Enemy("Zombie", "Undead monster", 10, 10, loot), 
                new Enemy("Bandit", "Rogue outlaw.", 10, 14, loot),
                new Enemy("Rabid Boar", "Ferocious beast", 20, 12, loot),
                new Enemy("Sand Worm", "Slithering worm from below", 5, 8, loot),
                new Enemy("Radioactive Matter", "An abhorent collection of garbage and ooze that moves", 8, 14, loot),
                new Enemy("Zombie", "Undead monster", 10, 10, loot),
                new Enemy("Bandit", "Rogue outlaw.", 10, 14, loot),
                new Enemy("Rabid Boar", "Ferocious beast", 20, 12, loot),
                new Enemy("Sand Worm", "Slithering worm from below", 5, 8, loot),
                new Enemy("Radioactive Matter", "An abhorent collection of garbage and ooze that moves", 8, 14, loot),
                new Enemy("Zombie", "Undead monster", 10, 10, loot),
                new Enemy("Bandit", "Rogue outlaw.", 10, 14, loot),
                new Enemy("Rabid Boar", "Ferocious beast", 20, 12, loot),
                new Enemy("Sand Worm", "Slithering worm from below", 5, 8, loot),
                new Enemy("Radioactive Matter", "An abhorent collection of garbage and ooze that moves", 8, 14, loot),
            ];

            // Shuffle two cards in deck 100 times
            Random r = new();
            for (var i = cards.Length - 1; i > 0; i--)
            {
                var j = r.Next(i + 1);
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }

            return new Deck<Enemy>("Enemy Deck", "Deck full of enemy cards.", new Stack<Enemy>(cards));
        }
    }
}
