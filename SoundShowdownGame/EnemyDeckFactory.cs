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
            // Mutant Loot
            Dictionary<Resource, int> mutantLoot = new Dictionary<Resource, int>
            {
                { new Resource("Leather", 3), 2 },
                { new Resource("Vial of Poison", 5), 1 }
            };
            // Raider Loot
            Dictionary<Resource, int> raiderLoot = new Dictionary<Resource, int>
            {
                { new Resource("Leather", 3), 2 },
                { new Resource("Adhesives", 3), 2 }
            };
            // Toxic Sludge Loot
            Dictionary<Resource, int> toxicSludgeLoot = new Dictionary<Resource, int>
            {
                { new Resource("Slime", 5), 3 }
            };
            // Sand Worm Loot
            Dictionary<Resource, int> sandWormLoot = new Dictionary<Resource, int>
            {
                { new Resource("Adhesives", 3), 3 }
            };
            // Venomfang Loot
            Dictionary<Resource, int> venomfangLoot = new Dictionary<Resource, int>
            {
                { new Resource("Fur", 5), 3 },
                { new Resource("Vial of Poison", 5), 1 }
            };
            // Automaton Loot
            Dictionary<Resource, int> automatonLoot = new Dictionary<Resource, int>
            {
                { new Resource("Metal Scrap", 5), 2 },
                { new Resource("Wire", 5), 1 },
                { new Resource("Batteries", 7), 1 }
            };
            // Cultist Loot
            Dictionary<Resource, int> cultistLoot = new Dictionary<Resource, int>
            {
                { new Resource("Leather", 3), 1 },
                { new Resource("Bone", 5), 2 },
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
    }
}
