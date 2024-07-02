using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class AttackEvent(Player player, AttackInfo attackInfo) : SoundShowdownEventArgs(SoundShowdownEventType.Attack)
    {
        public Player Player { get; private set; } = player;
        public AttackInfo Attack { get; private set; } = attackInfo;
    }
}
