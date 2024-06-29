using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public abstract class SoundShowdownEventArgs(SoundShowdownEventType eventType) : EventArgs
    {
        public SoundShowdownEventType EventType { get; private set; } = eventType;
    }
}
