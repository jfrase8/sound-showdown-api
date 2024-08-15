using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
        private Instrument? Instrument;
        private Upgrade? SuitUpgrade;
        private List<Upgrade> Accessories;
        private Dictionary<ResourceName, int> InventoryResources;
        private Dictionary<ResourceName, int> InventoryAccumulatedResources;
        private Dictionary<ItemName, int> InventoryItems;
        private int InventoryCoins;
        private Enemy? Enemy;
        private int BodyExp;

        public PlayerBuilder() 
        {
            // Defaults
            Id = $"Player_{IdCounter++}";
            Genre = null;
            Health = 10;
            Instrument = null;
            SuitUpgrade = null;
            Accessories = [];
            InventoryResources = [];
            InventoryAccumulatedResources = [];
            InventoryCoins = 0;
            Enemy = null;
            InventoryItems = [];
            BodyExp = 0;
        }

        public Player Build()
        {
            // TODO: Create an InventoryBuilder class
            Inventory inventory = new Inventory();
            inventory.ResourceInventory = InventoryResources;
            inventory.AccumulatedResources = InventoryAccumulatedResources;
            inventory.Coins = InventoryCoins;
            inventory.Items = InventoryItems;

            Player player = new Player(id: Id, genre: Genre, health: Health, instrument: Instrument, inventory: inventory, enemy: Enemy, suitUpgrade: SuitUpgrade, accessories: Accessories, bodyExp: BodyExp);
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
            Instrument = instrument;
            return this;
        }

        public PlayerBuilder WithInventoryResource(ResourceName resource, int amount)
        {
            InventoryResources[resource] = amount;
            return this;
        }

        public PlayerBuilder WithInventoryAccumulatedResource(ResourceName resource, int amount)
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

        public PlayerBuilder WithSuitUpgrade(Upgrade suitUpgrade)
        {
            SuitUpgrade = suitUpgrade;
            return this;
        }

        public PlayerBuilder WithAccessory(Upgrade accessory)
        {
            Accessories.Add(accessory);
            return this;
        }

        public PlayerBuilder WithInventoryItem(ItemName item, int amount)
        {
            InventoryItems[item] = amount;
            return this;
        }

        public PlayerBuilder WithBodyExperience(int exp)
        {
            BodyExp = exp;
            return this;
        }
    }
}
