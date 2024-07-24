using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public class AddedInstrumentExpEvent(Player player, int gainedExp, int instrumentLevel, int leftOverExp, bool leveledUp) : SoundShowdownEventArgs(SoundShowdownEventType.AddedInstrumentExp)
    {
        public Player Player { get; private set; } = player;
        public int GainedExp { get; private set; } = gainedExp;
        public int InstrumentLevel { get; private set; } = instrumentLevel;
        public int LeftOverExp { get; private set; } = leftOverExp;
        public bool LeveledUp { get; private set; } = leveledUp;
    }
}
