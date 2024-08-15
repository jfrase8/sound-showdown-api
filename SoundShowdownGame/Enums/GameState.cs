using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoundShowdownGame.Enums
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
        Awaiting_Player_Roll_For_Training,
        Awaiting_Player_Choose_Technique,
        Awaiting_Player_Shop,
        Awaiting_Player_Roll_Scavenge_Dice,
    }
}
