using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class PlayerBoughtItemEvent(Player player, Item item) : SoundShowdownEventArgs(SoundShowdownEventType.PlayerBoughtItem)
    {
        public Player Player { get; private set; } = player;
        public Item Item { get; private set; } = item;
    }
}
