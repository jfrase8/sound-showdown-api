using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class UpgradeSpaceFullEvent(Player player, Upgrade upgrade) : SoundShowdownEventArgs(SoundShowdownEventType.UpgradeSpaceFull)
    {
        public Player Player { get; private set; } = player;
        public Upgrade Upgrade { get; private set; } = upgrade;
    }
}
