using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class PlayerBoughtInstrumentEvent(Player player, Instrument instrument, Shop gameShop) : SoundShowdownEventArgs(SoundShowdownEventType.PlayerBoughtInstrument)
    {
        public Player Player { get; private set; } = player;
        public Instrument Instrument { get; private set; } = instrument;
        public Shop GameShop { get; private set; } = gameShop;
    }
}
