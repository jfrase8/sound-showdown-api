using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundShowdownGame.Enums;

namespace SoundShowdownGame.Builders
{
    public class EnemyDeckFactory
    {
        public static Deck<Enemy> CreateShuffledDeck()
        {
            // Mutant Loot
            Dictionary<ResourceName, int> mutantLoot = new Dictionary<ResourceName, int>
            {
                { ResourceName.Leather, 2 },
                { ResourceName.Vial_Of_Poison, 1 }
            };
            // Raider Loot
            Dictionary<ResourceName, int> raiderLoot = new Dictionary<ResourceName, int>
            {
                { ResourceName.Leather, 2 },
                { ResourceName.Adhesive, 2 }
            };
            // Toxic Sludge Loot
            Dictionary<ResourceName, int> toxicSludgeLoot = new Dictionary<ResourceName, int>
            {
                { ResourceName.Slime, 3 }
            };
            // Sand Worm Loot
            Dictionary<ResourceName, int> sandWormLoot = new Dictionary<ResourceName, int>
            {
                { ResourceName.Adhesive, 3 }
            };
            // Venomfang Loot
            Dictionary<ResourceName, int> venomfangLoot = new Dictionary<ResourceName, int>
            {
                { ResourceName.Fur, 3 },
                { ResourceName.Vial_Of_Poison, 1 }
            };
            // Automaton Loot
            Dictionary<ResourceName, int> automatonLoot = new Dictionary<ResourceName, int>
            {
                { ResourceName.Metal_Scrap, 2 },
                { ResourceName.Wire, 1 },
                { ResourceName.Batteries, 1 }
            };
            // Cultist Loot
            Dictionary<ResourceName, int> cultistLoot = new Dictionary<ResourceName, int>
            {
                { ResourceName.Leather, 1 },
                { ResourceName.Bone, 2 },
            };

            // Create deck
            Enemy[] cards =
            [
                new Enemy("Mutant", "An infected human with increased agility", 7, 8, mutantLoot, InstrumentType.String, InstrumentType.Brass, StatusEffect.Poison),
                new Enemy("Mutant", "An infected human with increased agility", 7, 8, mutantLoot, InstrumentType.String, InstrumentType.Brass, StatusEffect.Poison),
                new Enemy("Mutant", "An infected human with increased agility", 7, 8, mutantLoot, InstrumentType.String, InstrumentType.Brass, StatusEffect.Poison),
                new Enemy("Mutant", "An infected human with increased agility", 7, 8, mutantLoot, InstrumentType.String, InstrumentType.Brass, StatusEffect.Poison),
                new Enemy("Mutant", "An infected human with increased agility", 7, 8, mutantLoot, InstrumentType.String, InstrumentType.Brass, StatusEffect.Poison),
                new Enemy("Mutant", "An infected human with increased agility", 7, 8, mutantLoot, InstrumentType.String, InstrumentType.Brass, StatusEffect.Poison),
                new Enemy("Raider", "An outlaw that will ambush anyone for supplies", 6, 6, raiderLoot, InstrumentType.Vocal, InstrumentType.Wind, StatusEffect.None),
                new Enemy("Raider", "An outlaw that will ambush anyone for supplies", 6, 6, raiderLoot, InstrumentType.Vocal, InstrumentType.Wind, StatusEffect.None),
                new Enemy("Raider", "An outlaw that will ambush anyone for supplies", 6, 6, raiderLoot, InstrumentType.Vocal, InstrumentType.Wind, StatusEffect.None),
                new Enemy("Raider", "An outlaw that will ambush anyone for supplies", 6, 6, raiderLoot, InstrumentType.Vocal, InstrumentType.Wind, StatusEffect.None),
                new Enemy("Raider", "An outlaw that will ambush anyone for supplies", 6, 6, raiderLoot, InstrumentType.Vocal, InstrumentType.Wind, StatusEffect.None),
                new Enemy("Raider", "An outlaw that will ambush anyone for supplies", 6, 6, raiderLoot, InstrumentType.Vocal, InstrumentType.Wind, StatusEffect.None),
                new Enemy("Toxic Sludge", "A moving pile of gunk and trash", 3, 10, toxicSludgeLoot, InstrumentType.Percussion, InstrumentType.Vocal, StatusEffect.Poison),
                new Enemy("Toxic Sludge", "A moving pile of gunk and trash", 3, 10, toxicSludgeLoot, InstrumentType.Percussion, InstrumentType.Vocal, StatusEffect.Poison),
                new Enemy("Toxic Sludge", "A moving pile of gunk and trash", 3, 10, toxicSludgeLoot, InstrumentType.Percussion, InstrumentType.Vocal, StatusEffect.Poison),
                new Enemy("Toxic Sludge", "A moving pile of gunk and trash", 3, 10, toxicSludgeLoot, InstrumentType.Percussion, InstrumentType.Vocal, StatusEffect.Poison),
                new Enemy("Sand Worm", "A slithering worm from below that will suck the blood from you", 4, 4, sandWormLoot, InstrumentType.Percussion, InstrumentType.Wind, StatusEffect.Drain),
                new Enemy("Sand Worm", "A slithering worm from below that will suck the blood from you", 4, 4, sandWormLoot, InstrumentType.Percussion, InstrumentType.Wind, StatusEffect.Drain),
                new Enemy("Sand Worm", "A slithering worm from below that will suck the blood from you", 4, 4, sandWormLoot, InstrumentType.Percussion, InstrumentType.Wind, StatusEffect.Drain),
                new Enemy("Sand Worm", "A slithering worm from below that will suck the blood from you", 4, 4, sandWormLoot, InstrumentType.Percussion, InstrumentType.Wind, StatusEffect.Drain),
                new Enemy("Sand Worm", "A slithering worm from below that will suck the blood from you", 4, 4, sandWormLoot, InstrumentType.Percussion, InstrumentType.Wind, StatusEffect.Drain),
                new Enemy("Venomfang", "An infected animal with a venomous bite", 4, 7, venomfangLoot, InstrumentType.Wind, InstrumentType.String, StatusEffect.Poison),
                new Enemy("Venomfang", "An infected animal with a venomous bite", 4, 7, venomfangLoot, InstrumentType.Wind, InstrumentType.String, StatusEffect.Poison),
                new Enemy("Venomfang", "An infected animal with a venomous bite", 4, 7, venomfangLoot, InstrumentType.Wind, InstrumentType.String, StatusEffect.Poison),
                new Enemy("Venomfang", "An infected animal with a venomous bite", 4, 7, venomfangLoot, InstrumentType.Wind, InstrumentType.String, StatusEffect.Poison),
                new Enemy("Venomfang", "An infected animal with a venomous bite", 4, 7, venomfangLoot, InstrumentType.Wind, InstrumentType.String, StatusEffect.Poison),
                new Enemy("Automaton", "A sentient robot with deadly weapons", 10, 9, automatonLoot, InstrumentType.Brass, InstrumentType.Electronic, StatusEffect.Shock),
                new Enemy("Automaton", "A sentient robot with deadly weapons", 10, 9, automatonLoot, InstrumentType.Brass, InstrumentType.Electronic, StatusEffect.Shock),
                new Enemy("Automaton", "A sentient robot with deadly weapons", 10, 9, automatonLoot, InstrumentType.Brass, InstrumentType.Electronic, StatusEffect.Shock),
                new Enemy("Cultist", "A lunatic part of a secret organization", 5, 7, cultistLoot, InstrumentType.Electronic, InstrumentType.Percussion, StatusEffect.Drain),
                new Enemy("Cultist", "A lunatic part of a secret organization", 5, 7, cultistLoot, InstrumentType.Electronic, InstrumentType.Percussion, StatusEffect.Drain),
                new Enemy("Cultist", "A lunatic part of a secret organization", 5, 7, cultistLoot, InstrumentType.Electronic, InstrumentType.Percussion, StatusEffect.Drain),
                new Enemy("Cultist", "A lunatic part of a secret organization", 5, 7, cultistLoot, InstrumentType.Electronic, InstrumentType.Percussion, StatusEffect.Drain),
                new Enemy("Cultist", "A lunatic part of a secret organization", 5, 7, cultistLoot, InstrumentType.Electronic, InstrumentType.Percussion, StatusEffect.Drain),
                new Enemy("Cultist", "A lunatic part of a secret organization", 5, 7, cultistLoot, InstrumentType.Electronic, InstrumentType.Percussion, StatusEffect.Drain),
            ];

            Random r = new();
            for (var i = cards.Length - 1; i > 0; i--)
            {
                var j = r.Next(i + 1);
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }

            return new Deck<Enemy>("Enemy Deck", "Deck full of enemy cards.", new Stack<Enemy>(cards));
        }

        public static Enemy CreateTestEnemy(int health, int damage, InstrumentType weakness, InstrumentType resistance, StatusEffect statusEffect)
        {
            // Test Loot
            Dictionary<ResourceName, int> testLoot = new Dictionary<ResourceName, int>
            {
                { ResourceName.Leather, 2 },
                { ResourceName.Vial_Of_Poison, 1 }
            };

            return new Enemy("Test Enemy", "Enemy created for testing", health, damage, testLoot, weakness, resistance, statusEffect);
        }
    }
}
