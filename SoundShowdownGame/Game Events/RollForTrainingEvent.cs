using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class RollForTrainingEvent(Player player, int firstRoll, int secondRoll, bool overPracticed) : SoundShowdownEventArgs(SoundShowdownEventType.RollForTraining)
    {
        public Player Player { get; private set; } = player;
        public int FirstRoll { get; private set; } = firstRoll;
        public int SecondRoll { get; private set; } = secondRoll;
        public bool OverPracticed { get; private set; } = overPracticed;
    }
}
