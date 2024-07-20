using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame
{
    public enum GameState
    {
        Awaiting_Player_Choose_Genre,
        Awaiting_Player_Choose_Action,
        Awaiting_Player_Attack,
        Awaiting_Player_Fight_Or_End_Action,
        Awaiting_Player_Choose_Upgrade,
        Awaiting_Player_Replace_Upgrade,
        Awaiting_Player_Choose_Scrap_Resource,
    }
}
