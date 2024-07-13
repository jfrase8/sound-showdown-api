using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public partial class Inventory(Dictionary<Resource, int> accumulatedInventory)
    {
        public Dictionary<Resource, int> ResourceInventory { get; set; } = []; // Dictionary of resources that is currently in the players inventory
        public Dictionary<Resource, int> AccumulatedResources { get; set; } = accumulatedInventory; // Dictionary of resources that the player has collected while fighting enemies or scavenging
        public int Coins { get; set; } = 20;

        public Inventory() : this([]) { } // If no resource dict is provided, give it an empty dict


        // Adds Resource dictionary to Resource Dictionary in Inventory - Created for fun (should be a method)
        public static Inventory operator +(Inventory inventory, Dictionary<Resource, int> dict)
        {
            Inventory copy = new(inventory.ResourceInventory);

            // Combine two dictionaries
            foreach (var kvp in dict)
            {
                // If player already has at least 1 of this resource, add to that resource
                copy.AccumulatedResources[kvp.Key] = copy.AccumulatedResources.GetValueOrDefault(kvp.Key, kvp.Value) + kvp.Value;
            }
            return copy;
        }

        // Adds a single resource to inventory
        public static Inventory operator +(Inventory inventory, Resource resource)
        {
            Inventory copy = new(inventory.ResourceInventory);
            copy.ResourceInventory[resource] = copy.ResourceInventory.GetValueOrDefault(resource, 1) + 1;
            return copy;
        }

        // Add the accumulated resources to your resource inventory then reset accumulated resources
        public void GainResources()
        {
            ResourceInventory = AccumulatedResources.ToDictionary(entry => entry.Key, entry => entry.Value);
            AccumulatedResources = [];
        }

        // Checks whether the player checks all boxes on cost of an item
        public void ValidateInventory<T>(InventoryType type, T itemWithCost)
        {
            if (itemWithCost == null) throw new SoundShowdownException("Item is null");

            if (type == InventoryType.Resource)
            {
                if (itemWithCost is Upgrade upgrade)
                {
                    ValidateUpgradeCost(upgrade);
                }
                else throw new SoundShowdownException($"Item being built is of an unknown type:{itemWithCost.GetType()}");
            }
            else if (type == InventoryType.Coin)
            {
                if (itemWithCost is Instrument instrument)
                {
                    if (Coins < instrument.Cost) throw new SoundShowdownException("Player does not have enough money to buy this instrument.");
                }
                else throw new SoundShowdownException($"Item being bought is of an unknown type:{itemWithCost.GetType()}");
            }
        }

        // Checks if the player has the required resources to build an upgrade
        public void ValidateUpgradeCost(Upgrade upgrade)
        {
            foreach (Resource key in upgrade.BuildCost.Keys)
            {
                if (ResourceInventory.TryGetValue(key, out int amount))
                {
                    if (amount < upgrade.BuildCost[key]) throw new SoundShowdownException($"Player does not have all the resources to build this upgrade. Needs {upgrade.BuildCost[key] - amount} more {key}.");
                }
                else throw new SoundShowdownException($"Player does not have all the resources to build this upgrade. Missing {key}");
            }
        }

        // Removes needed resources to build an upgrade from player inventory
        public void BuildUpgrade(Upgrade upgrade)
        {
            foreach (Resource key in upgrade.BuildCost.Keys)
            {
                ResourceInventory[key] -= upgrade.BuildCost[key];
                if (ResourceInventory[key] == 0) ResourceInventory.Remove(key);
            }
        }
    }
}
