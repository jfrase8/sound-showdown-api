using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class Inventory(Dictionary<ResourceName, int> accumulatedInventory)
    {
        public Dictionary<ResourceName, int> ResourceInventory { get; set; } = []; // Dictionary of resources that is currently in the players inventory
        public Dictionary<ResourceName, int> AccumulatedResources { get; set; } = accumulatedInventory; // Dictionary of resources that the player has collected while fighting enemies or scavenging
        public int Coins { get; set; } = 20;

        public Inventory() : this([]) { } // If no resource dict is provided, give it an empty dict


        // Adds Resource dictionary to Resource Dictionary in Inventory - Created for fun (should be a method)
        public static Inventory operator +(Inventory inventory, Dictionary<ResourceName, int> dict)
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
        public static Inventory operator +(Inventory inventory, ResourceName resource)
        {
            Inventory copy = new(inventory.ResourceInventory);
            copy.ResourceInventory[resource] = copy.ResourceInventory.GetValueOrDefault(resource, 1);
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
                else if (itemWithCost is InstrumentType instrumentType)
                {
                    if (!ValidateFixCost(instrumentType)) throw new SoundShowdownException($"Player does not have the resources to fix an instrument of {instrumentType} type.");
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
        private void ValidateUpgradeCost(Upgrade upgrade)
        {
            foreach (ResourceName key in upgrade.BuildCost.Keys)
            {
                if (ResourceInventory.TryGetValue(key, out int amount))
                {
                    if (amount < upgrade.BuildCost[key]) throw new SoundShowdownException($"Player does not have all the resources to build this upgrade. Needs {upgrade.BuildCost[key] - amount} more {key}.");
                }
                else throw new SoundShowdownException($"Player does not have all the resources to build this upgrade. Missing {key}");
            }
        }

        private bool ValidateFixCost(InstrumentType type)
        {
            ResourceName resourceToCheck = type switch
            {
                InstrumentType.Brass => ResourceName.Tin_Can,
                InstrumentType.Electronic => ResourceName.Wire,
                InstrumentType.Vocal => ResourceName.Wire,
                InstrumentType.Wind => ResourceName.Glass,
                InstrumentType.Percussion => ResourceName.Plastic,
                InstrumentType.String => ResourceName.String,
                _ => throw new SoundShowdownException($"Instrument type: {type}, is not a valid type.")
            };
            bool hasAdhesive = false;
            foreach (KeyValuePair<ResourceName, int> kvp in ResourceInventory)
            {
                if (kvp.Key == resourceToCheck)
                {
                    if (kvp.Value < 2) return false;
                }
                if (kvp.Key == ResourceName.Adhesive)
                {
                    hasAdhesive = true;
                }
            }
            if (!hasAdhesive) return false;
            else return true;
        }

        // Removes needed resources to build an upgrade from player inventory
        public void BuildUpgrade(Upgrade upgrade)
        {
            foreach (ResourceName key in upgrade.BuildCost.Keys)
            {
                ResourceInventory[key] -= upgrade.BuildCost[key];
                if (ResourceInventory[key] == 0) ResourceInventory.Remove(key);
            }
        }

        public void FixInstrument(InstrumentType type)
        {
            ResourceName resourceToUse = type switch
            {
                InstrumentType.Brass => ResourceName.Tin_Can,
                InstrumentType.Electronic => ResourceName.Wire,
                InstrumentType.Vocal => ResourceName.Wire,
                InstrumentType.Wind => ResourceName.Glass,
                InstrumentType.Percussion => ResourceName.Plastic,
                InstrumentType.String => ResourceName.String,
                _ => throw new SoundShowdownException($"Instrument type: {type}, is not a valid type.")
            };

            foreach (KeyValuePair<ResourceName, int> kvp in ResourceInventory)
            {
                if (kvp.Key == resourceToUse)
                {
                    ResourceInventory[kvp.Key] -= 2;
                    if (ResourceInventory[kvp.Key] == 0) ResourceInventory.Remove(kvp.Key);
                }
                if (kvp.Key == ResourceName.Adhesive)
                {
                    ResourceInventory[kvp.Key]--;
                    if (ResourceInventory[kvp.Key] == 0) ResourceInventory.Remove(kvp.Key);
                }
            }
        }
    }
}
