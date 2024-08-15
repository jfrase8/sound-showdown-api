using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoundShowdownGame.Enums;

namespace SoundShowdownGame
{
    public class TradeWithTraderEvent(Player player, ResourceName[] fourResources, ResourceName newResource) : SoundShowdownEventArgs(SoundShowdownEventType.TradeWithTrader)
    {
        public Player Player { get; private set; } = player;
        public ResourceName[] FourResources { get; private set; } = fourResources;
        public ResourceName NewResource { get; private set;} = newResource;
    }
}
