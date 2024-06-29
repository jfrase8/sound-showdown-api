using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class Inventory(Dictionary<Resource, int> resourceInventory)
    {
        public Dictionary<Resource, int> ResourceInventory { get; set; } = resourceInventory; // Dictionary of resources that is currently in the players inventory
        public Dictionary<Resource, int> AccumulatedResources { get; set; } = []; // Dictionary of resources that the player has collected while fighting enemies or scavenging
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
                if (copy.AccumulatedResources.ContainsKey(kvp.Key))
                {
                    copy.AccumulatedResources[kvp.Key] += kvp.Value;
                }
                // Otherwise, create a new entry with that resource and the amount that player is gaining
                else
                {
                    copy.AccumulatedResources[kvp.Key] = kvp.Value;
                }
            }
            return copy;
        }

        // Add the accumulated resources to your resource inventory then reset accumulated resources
        public void GainResources()
        {
            ResourceInventory = AccumulatedResources.ToDictionary(entry => entry.Key, entry => entry.Value);
            AccumulatedResources = [];
        }
    }
}
