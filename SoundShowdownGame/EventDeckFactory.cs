using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class EventDeckFactory
    {
        public static Deck<EventCard> CreateShuffledDeck()
        {
            EventCard[] cards =
            [
                new EventCard(EventCardName.The_Stash, "You have stumbled upon a metal box. You open it.", "Gain 3 metal scrap", TheStashOutcome),
                new EventCard(EventCardName.The_Attic, "You hear something from above. You go to investigate and find another player.", "Battle the player to your right. Whoever wins gets 20 coins", TheAtticOutcome),
            ];

            Random r = new();
            for (var i = cards.Length - 1; i > 0; i--)
            {
                var j = r.Next(i + 1);
                (cards[i], cards[j]) = (cards[j], cards[i]);
            }

            return new Deck<EventCard>("Event Card Deck", "Events are drawn if you roll the event icon on the scavenge dice", new Stack<EventCard>(cards));
        }

        private static void TheStashOutcome(Player player)
        {
            var scrap = new Dictionary<ResourceName, int>
            {
                {ResourceName.Metal_Scrap, 3 }
            };

            player.Inventory += scrap;
        }
        private static void TheAtticOutcome(Player player)
        {
            // TODO : Create a way to battle another player
            player.Inventory.Coins += 20;
        }
    }
}
