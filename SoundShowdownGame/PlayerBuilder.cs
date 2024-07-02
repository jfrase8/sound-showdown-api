using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class PlayerBuilder
    {
        private static int IdCounter = 1;

        private string Id;
        private GenreName? Genre;
        private int Health;
        private List<Instrument> Instruments;
        private Dictionary<Resource, int> InventoryResources;
        private Dictionary<Resource, int> InventoryAccumulatedResources;
        private int InventoryCoins;
        private Enemy? Enemy;

        public PlayerBuilder() 
        {
            // Defaults
            Id = $"Player_{IdCounter++}";
            Genre = null;
            Health = 10;
            Instruments = new List<Instrument>();
            InventoryResources = new Dictionary<Resource, int>();
            InventoryAccumulatedResources = new Dictionary<Resource, int>();
            InventoryCoins = 0;
            Enemy = null;
        }

        public Player Build()
        {
            // TODO: Create an InventoryBuilder class
            Inventory inventory = new Inventory();
            inventory.ResourceInventory = InventoryResources;
            inventory.AccumulatedResources = InventoryAccumulatedResources;
            inventory.Coins = InventoryCoins;

            Player player = new Player(id: Id, genre: Genre, health: Health, instruments: Instruments, inventory: inventory, enemy: Enemy);
            return player;
        }

        public PlayerBuilder WithId(string id)
        {
            Id = id;
            return this;
        }

        public PlayerBuilder WithGenre(GenreName? genre)
        {
            Genre = genre;
            return this;
        }

        public PlayerBuilder WithHealth(int health)
        {
            Health = health;
            return this;
        }

        public PlayerBuilder WithInstrument(Instrument instrument)
        {
            Instruments.Add(instrument);
            return this;
        }

        public PlayerBuilder WithInventoryResource(Resource resource, int amount)
        {
            InventoryResources[resource] = amount;
            return this;
        }

        public PlayerBuilder WithInventoryAccumulatedResource(Resource resource, int amount)
        {
            InventoryAccumulatedResources[resource] = amount;
            return this;
        }

        public PlayerBuilder WithInventoryCoins(int coins)
        {
            InventoryCoins = coins;
            return this;
        }

        public PlayerBuilder WithEnemy(Enemy? enemy)
        {
            Enemy = enemy;
            return this;
        }

    }
}
