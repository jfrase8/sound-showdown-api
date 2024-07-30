using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public delegate void EventCardOutcome(Player player);

    public class EventCard(EventCardName name, string eventDescription, string outcomeDescription, EventCardOutcome outcome)
    {
        public EventCardName Name { get; set; } = name;
        public string eventDescription { get; set; } = eventDescription;
        public string OutcomeDescription { get; set; } = outcomeDescription;
        public EventCardOutcome Outcome { get; set; } = outcome;
    }
}
